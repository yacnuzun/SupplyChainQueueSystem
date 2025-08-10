# SupplyChainQueueSystem

**SupplyChainQueueSystem**, tedarik zinciri süreçleri gibi yüksek hacimli ve paralel işlem gerektiren senaryolarda mesaj tabanlı haberleşme sağlayan, .NET 8 ile geliştirilmiş mikroservis mimarisine sahip bir kuyruk yönetim sistemidir. RabbitMQ, Docker, Unit of Work ve Domain-Driven Design (DDD) prensipleriyle inşa edilmiştir.

## 🚀 Proje Amacı

- Mikroservis mimarisi üzerine gerçek dünyaya yakın bir yapı kurmak  
- RabbitMQ üzerinden asenkron haberleşmeyi yönetmek  
- SOLID prensiplerine uygun, modüler ve ölçeklenebilir bir yapı oluşturmak  
- Katmanlı mimari, validation, logging ve unit testing gibi yazılım geliştirme disiplinlerini uygulamak

---

## ⚙️ Kullanılan Teknolojiler ve Araçlar

| Teknoloji         | Açıklama                                      |
|------------------|-----------------------------------------------|
| .NET 8           | Temel backend framework                       |
| RabbitMQ         | Mesajlaşma altyapısı                          |
| Docker / Compose | Servis konteynerleştirme ve orkestrasyon      |
| FluentValidation | DTO seviyesinde veri doğrulama                |
| MailKit          | SMTP ile e-posta gönderimi                    |
| Unit of Work     | Veri erişim yönetimi                          |
| Serilog          | Loglama (planlandı)                           |
| xUnit            | Unit test altyapısı                           |

---

## 🧱 Proje Yapısı

```bash
/MicroservicesQueue
│
├── src
│ ├── AccountApi
│ ├── BillApi
│ ├── BuyerApi
│ ├── FinancialApi
│ ├── SharedLibraries
│ └── SupplierApi
├── tests
│ ├── AccountUnitApi
│
├── docker-compose.yml
└── README.md
```
- 📦 Mikroservisler
    - **AccountApi**: Kullanıcı kayıt ve giriş işlemlerini kontrol eder.
    - **BillApi**: Fatura kesimi yapar.
    - **FinancialApi**: Finans kurumlarının tedarikçilerin attıkları isteklere cevap verdiği servis.
    - **SupplierApi**: Tedarikçilerin erken ödeme talebi açmalarını sağlayan ve kuyruğu dinleyerek alınan faturaları bildiren servis.

- 🔁 Ortak Bileşenler
    - **SharedLibraries**: Mikroservisler arasında ortak kullanılan tüm yardımcı sınıfları içerir:

      - **Interfaces**: Repository, Mail, Hashing gibi servis soyutlamaları.

      - **Event Models**: RabbitMQ mesajlaşma altyapısı için kullanılan event sınıfları.

      - **BaseEntity**: Ortak entity özelliklerini tanımlar (örneğin Id, CreatedDate).

      - **Security**: JWT token yönetimi, TokenOptions ve şifreleme algoritmaları (SHA256, HMAC).

      - **Result Yapısı**: Başarı/başarısızlık durumları için standart sonuç modelleri.

      - **MailService**: SMTP üzerinden e-posta gönderimi yapan yapı.

      - **Generic Repository & Unit of Work**: Veritabanı işlemleri için ortak veri erişim katmanı.
---

## 🛠️ Kurulum (Docker ile Çalıştırma)

> Projeyi çalıştırmak için Docker ve .NET 8 SDK yüklü olmalıdır.

1. Repoyu klonlayın:

    ```bash
        git clone https://github.com/kullanici-adi/MicroservicesQueue.git
        cd MicroservicesQueue
    ```
2. Docker'ı Kurun:
    ```bash
        docker-compose up --build
    ```
---
## ✅ Özellikler

-  RabbitMQ Publisher / Consumer yapısı
-  DTO validasyonları (FluentValidation)
-  Unit of Work ve Generic Repository altyapısı
-  Temel test altyapısı (xUnit)
---
## 🧪 Testler

- xUnit ile yazılmış unit test örnekleri mevcuttur.
- Test coverage ileride artırılacaktır.
---
## ✉️ Mail Servisi

- NotificationService, gelen mesajlara göre SMTP üzerinden e-posta gönderimi yapar.

- MailKit kullanılmaktadır, appsettings.json üzerinden SMTP ayarları yapılabilir.


