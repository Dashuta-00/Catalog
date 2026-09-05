import { textSummary } from 'https://jslib.k6.io/k6-summary/0.0.1/index.js';
import { check, sleep } from 'k6';
import http from 'k6/http';

export let options = {
    stages: [
        { duration: '30s', target: 1 },   // Плавный старт
        { duration: '9m', target: 1 },    // Стабильная нагрузка 1 пользователь = ~10-20 запросов/мин
        { duration: '30s', target: 0 },   // Завершение
    ],
    setupTimeout: '30s',
    thresholds: {
        http_req_duration: ['p(95)<5000'], // POST запросы могут быть медленнее
        http_req_failed: ['rate<0.05'],    // Менее 5% ошибок для создания товаров
    },
};

const BASE_URL = __ENV.BASE_URL_CATALOG || 'http://catalog_api:8080';

// Статические данные брендов и категорий
const BRANDS = [
    { "title": "Polaris", "id": "b0000001-0000-0000-0000-000000000001" },
    { "title": "Redmond", "id": "b0000001-0000-0000-0000-000000000002" },
    { "title": "Vitek", "id": "b0000001-0000-0000-0000-000000000003" },
    { "title": "Scarlett", "id": "b0000001-0000-0000-0000-000000000004" },
    { "title": "Снеж", "id": "b0000001-0000-0000-0000-000000000005" },
    { "title": "Мир детства", "id": "b0000001-0000-0000-0000-000000000006" },
    { "title": "Светозар", "id": "b0000001-0000-0000-0000-000000000007" },
    { "title": "Детский мир", "id": "b0000001-0000-0000-0000-000000000008" },
    { "title": "Золотое яблоко", "id": "b0000001-0000-0000-0000-000000000009" },
    { "title": "Славянка", "id": "b0000001-0000-0000-0000-000000000010" }
];

const CATEGORIES = [
    { "title": "Бытовая техника", "id": "c0000001-0000-0000-0000-000000000001" },
    { "title": "Электроника", "id": "c0000001-0000-0000-0000-000000000002" },
    { "title": "Одежда", "id": "c0000001-0000-0000-0000-000000000003" },
    { "title": "Детские товары", "id": "c0000001-0000-0000-0000-000000000004" },
    { "title": "Косметика и уход", "id": "c0000001-0000-0000-0000-000000000005" }
];

// Шаблоны товаров для каждой категории
const PRODUCT_TEMPLATES = {
    "Бытовая техника": [
        "Пылесос", "Микроволновая печь", "Блендер", "Тостер", "Кофеварка",
        "Утюг", "Фен", "Мультиварка", "Чайник электрический", "Миксер"
    ],
    "Электроника": [
        "Наушники", "Колонки", "Зарядное устройство", "Кабель USB", "Роутер Wi-Fi",
        "Веб-камера", "Мышь компьютерная", "Клавиатура", "Флэш-накопитель", "Планшет"
    ],
    "Одежда": [
        "Футболка", "Джинсы", "Свитер", "Куртка", "Платье",
        "Рубашка", "Брюки", "Кроссовки", "Ботинки", "Кепка"
    ],
    "Детские товары": [
        "Игрушка развивающая", "Пазл", "Конструктор", "Кукла", "Машинка",
        "Мяч детский", "Книга детская", "Набор для творчества", "Велосипед детский", "Самокат"
    ],
    "Косметика и уход": [
        "Крем для лица", "Шампунь", "Зубная паста", "Мыло", "Лосьон для тела",
        "Маска для волос", "Скраб", "Сыворотка", "Тональный крем", "Помада"
    ]
};

export function setup() {
    console.log('🔧 Проверка доступности Catalog API...');

    // Проверяем доступность API
    let healthCheck = http.get(`${BASE_URL}/api/v1/brands`);
    if (healthCheck.status !== 200) {
        throw new Error(`Catalog API недоступен: ${healthCheck.status} - ${healthCheck.status_text}`);
    }

    console.log('✅ Catalog API доступен');
    console.log('🛒 Начинаем создание товаров в каталоге (10-20 товаров/минуту)');
    return { baseUrl: BASE_URL };
}

export default function (data) {
    // Генерируем реалистичный товар
    const product = generateRandomProduct();

    const response = http.post(
        `${data.baseUrl}/api/v1/CatalogItem`,
        JSON.stringify(product),
        {
            headers: {
                'Content-Type': 'application/json',
            },
        }
    );

    // Проверяем результат создания товара
    const checkResults = check(response, {
        'товар создан успешно': (r) => r.status === 201 || r.status === 200,
        'время создания приемлемое': (r) => r.timings.duration < 5000,
        'получен валидный JSON': (r) => {
            try {
                JSON.parse(r.body);
                return true;
            } catch (e) {
                return false;
            }
        },
        'заголовки корректные': (r) =>
            r.headers['Content-Type'] && r.headers['Content-Type'].includes('application/json'),
    });

    // Логирование успешных созданий
    if (response.status === 200 || response.status === 201) {
        console.log(`✅ Создан товар: ${product.title} (${product.brand.title} - ${product.category.title}) - ${product.price}₽`);
    } else {
        console.error(`❌ Ошибка создания товара: ${response.status} - ${response.status_text}`);
        console.error(`   Товар: ${product.title}`);
        console.error(`   Тело ответа: ${response.body.substring(0, 200)}...`);
    }

    // Логируем медленные запросы
    if (response.timings.duration > 3000) {
        console.warn(`🐌 Медленное создание товара: ${response.timings.duration}ms`);
    }

    // Пауза между созданием товаров (3-6 секунд = 10-20 товаров в минуту)
    sleep(Math.random() * 3 + 3); // 3-6 секунд между запросами
}

// Функция генерации случайного товара
function generateRandomProduct() {
    const randomBrand = BRANDS[Math.floor(Math.random() * BRANDS.length)];
    const randomCategory = CATEGORIES[Math.floor(Math.random() * CATEGORIES.length)];
    const productTemplates = PRODUCT_TEMPLATES[randomCategory.title];
    const randomProduct = productTemplates[Math.floor(Math.random() * productTemplates.length)];

    // Генерируем цену в зависимости от категории
    const priceRanges = {
        "Бытовая техника": [5000, 50000],
        "Электроника": [1000, 25000],
        "Одежда": [500, 8000],
        "Детские товары": [300, 15000],
        "Косметика и уход": [200, 5000]
    };

    const [minPrice, maxPrice] = priceRanges[randomCategory.title];
    const price = Math.floor(Math.random() * (maxPrice - minPrice) + minPrice);

    return {
        title: `${randomProduct} ${randomBrand.title} ${Math.floor(Math.random() * 1000) + 1}`,
        shortDescription: `Качественный ${randomProduct.toLowerCase()} от бренда ${randomBrand.title}`,
        fullDescription: `Подробное описание товара ${randomProduct.toLowerCase()} бренда ${randomBrand.title}. Высокое качество, современный дизайн, надежность. Идеально подходит для повседневного использования.`,
        imageUrl: `https://example.com/images/${randomProduct.toLowerCase().replace(/\s+/g, '-')}.jpg`,
        brand: randomBrand,
        category: randomCategory,
        price: price
    };
}

export function teardown(data) {
    console.log('📊 Создание товаров в каталоге завершено');
    console.log(`🔗 API URL: ${data.baseUrl}`);
}

export function handleSummary(data) {
    return {
        'catalog-create-items-results.json': JSON.stringify(data, null, 2),
        'stdout': textSummary(data, { indent: ' ', enableColors: true }),
    };
}