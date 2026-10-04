# SupplyChainQueueSystem

![CI](https://github.com/yacnuzun/SupplyChainQueueSystem/actions/workflows/ci.yml/badge.svg)

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
| GitHub Actions   | CI: otomatik build ve test                    |
| FluentValidation | DTO seviyesinde veri doğrulama                |
| MailKit          | SMTP ile e-posta gönderimi                    |
| Unit of Work     | Veri erişim yönetimi                          |
| Serilog          | Loglama (planlandı)                           |
| xUnit            | Unit test altyapısı                           |

---

## 🧱 Proje Yapısı

```bash
/SupplyChainQueueSystem
│
├── .github
│ └── workflows
│   └── ci.yml
├── src
│ ├── AccountApi
│ ├── BillApi
│ ├── BuyerAPI
│ ├── FinancialAPI
│ ├── Shared
│ └── SupplierAPI
├── tests
│ └── AccountUnitTest
│
├── MicrosevicesQueque.sln
├── docker-compose.yml
└── README.md
```
- 📦 Mikroservisler
    - **AccountApi**: Kullanıcı kayıt ve giriş işlemlerini kontrol eder.
    - **BillApi**: Fatura kesimi yapar.
    - **FinancialApi**: Finans kurumlarının tedarikçilerin attıkları isteklere cevap verdiği servis.
    - **SupplierApi**: Tedarikçilerin erken ödeme talebi açmalarını sağlayan ve kuyruğu dinleyerek alınan faturaları bildiren servis.

- 🔁 Ortak Bileşenler
    - **Shared**: Mikroservisler arasında ortak kullanılan tüm yardımcı sınıfları içerir:

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
        git clone https://github.com/yacnuzun/SupplyChainQueueSystem.git
        cd SupplyChainQueueSystem
    ```
2. Ortam değişkenlerini hazırlayın (gizli değerler `.env` dosyasında tutulur, repoya eklenmez):
    ```bash
        cp .env.example .env
    ```
    `.env.example` yerel geliştirme için çalışan varsayılan değerlerle gelir. Mail gönderimi için isteğe bağlı olarak `EMAIL_SENDER` ve `EMAIL_APP_PASSWORD` alanlarını doldurabilirsiniz, boş bırakılırsa servisler çalışır ama e-posta gönderilmez.
3. Servisleri ayağa kaldırın:
    ```bash
        docker compose up --build
    ```
---
## ✅ Özellikler

-  RabbitMQ Publisher / Consumer yapısı
-  DTO validasyonları (FluentValidation)
-  Unit of Work ve Generic Repository altyapısı
-  JWT tabanlı kimlik doğrulama ve rol bazlı yetkilendirme (Admin, Buyer, Supplier)
-  Temel test altyapısı (xUnit)
-  GitHub Actions ile her push ve pull request'te otomatik build ve test (CI)
---
## 🔐 Authentication ve Yetkilendirme

- **AccountApi** kullanıcı kaydı ve girişini yönetir, başarılı girişte **JWT** üretir. Şifreler hash ve salt ile saklanır.
- Diğer servisler (BillApi, BuyerAPI, SupplierAPI) aynı token'ı doğrular. Doğrulama ayarları `Shared` kütüphanesindeki `TokenValidate` sınıfında ortaktır.
- Yetkilendirme `[Authorize(Roles = ...)]` attribute'larıyla rol bazlı yapılır:

| Rol | Erişebildiği alanlar |
|---|---|
| **Admin** | Claim yönetimi ve mail şablonu yönetimi uçları (AccountApi) |
| **Buyer** | Alıcıya ait uçlar (BuyerAPI) ve fatura işlemlerinin alıcı tarafı (BillApi) |
| **Supplier** | Tedarikçiye ait uçlar (SupplierAPI) ve fatura işlemlerinin tedarikçi tarafı (BillApi) |

---
## 🧪 Testler

- xUnit ile yazılmış unit test örnekleri mevcuttur.
- Test coverage ileride artırılacaktır.
---
## ✉️ Mail Servisi

- NotificationService, gelen mesajlara göre SMTP üzerinden e-posta gönderimi yapar.

- MailKit kullanılmaktadır, SMTP ayarları `.env` dosyasındaki `EMAIL_SENDER` ve `EMAIL_APP_PASSWORD` değerleriyle verilir (Gmail için uygulama şifresi kullanılmalıdır).

✅ Docker container eklendi (Commit: 58be5dd5, 2025-08-30)


### 🚀 Yeni Eklenenler
- [2025-08-30] Docker Container Eklendi.


## 🛣️ Roadmap

Proje halen geliştirme aşamasındadır. Süreç şeffaf bir şekilde commit geçmişi üzerinden takip edilebilir.  
Gelecek adımlar aşağıdaki gibi planlanmıştır:

- [x] Mikroservis yapısının kurulması  
- [x] RabbitMQ entegrasyonu  
- [x] Dockerfile ve docker-compose yapılandırmaları  
- [x] CI pipeline (GitHub Actions: build + test)  
- [x] Authentication (JWT + role-based authorization)  
- [ ] CD pipeline (deployment)  
- [ ] Test coverage oranının artırılması 

## 🐳 Docker Teknik Detayları

Projede her mikroservis için ayrı bir **Dockerfile** oluşturulmuştur.  
Ayrıca `docker-compose.yml` ile tüm servisler aynı anda ayağa kaldırılabilmektedir.  

### Yapılan Düzenlemeler
- `Dockerfile` → Her servis için publish edilen `.dll` dosyaları Kestrel üzerinde çalışacak şekilde yapılandırıldı.  
- `docker-compose.yml` → PostgreSQL, RabbitMQ ve mikroservisler aynı network üzerinde tanımlandı, servisler healthcheck ile sıralı başlar. Gizli değerler `.env` dosyasından okunur.  
- `.dockerignore` → Gereksiz dosyaların (bin, obj, user secrets vb.) imaja dahil edilmesi engellendi.  
- Kestrel URL ayarları güncellendi (örn: `http://0.0.0.0:5001`).  

### Çalışan Servisler (docker-compose)
- **AccountApi** → `http://localhost:8081`  
- **BillApi** → `http://localhost:8083`  
- **BuyerApi** → `http://localhost:8084`  
- **FinancialApi** → `http://localhost:8085`  
- **SupplierApi** → `http://localhost:8086`  
- **RabbitMQ Management UI** → `http://localhost:15672` (varsayılan: guest / guest, port ve kimlik bilgileri `.env` ile değiştirilebilir)