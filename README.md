# 🛒 BookStore - Readme پروژه

**یک فروشگاه کتاب آنلاین ساده و واکنش‌گرا با ASP.NET Core MVC و EF Core** — شامل صفحات مشتری، سبد خرید، ثبت‌نام دوره/سفارش، مدیریت محصولات و پرداخت (Stripe).

---

## 🚀 خلاصه پروژه (فارسی)
BookStore یک نمونه فروشگاه کتاب است که امکانات زیر را پوشش می‌دهد:
- صفحه اصلی محصولات (کتاب) با کاور، عنوان، قیمت و دکمه مشاهده
- صفحه جزئیات کتاب با توضیحات، فهرست سرفصل‌ها (Chapters) و دکمه افزودن به سبد
- سبد خرید: افزودن، ویرایش تعداد، نمایش مجموع قیمت
- پرداخت: ارسال به Stripe (پیاده‌سازی API) و صفحه تایید سفارش
- ثبت سفارش (Order) و نمایش سفارش‌های کاربر
- احراز هویت با Identity (ثبت‌نام/ورود) و نقش‌های پایه (Admin/Customer)
- پنل ادمین: مدیریت کتاب‌ها، دسته‌ها، سفارشات و کاربران
- نوتیفیکیشن‌های تعاملی با SweetAlert / Awesome Notifications

---

## 📦 ساختار پیشنهادی
- `Models/` — BookModel, ChapterModel, CategoryModel, OrderModel, OrderItemModel, ApplicationUser
- `Data/ApplicationDbContext.cs` — DbContext (DbSetها)
- `Controllers/` — HomeController, BookController, CartController, CheckoutController, OrderController, AdminController, Account (Identity)
- `Views/` — Razor Views (Home, Book, Cart, Checkout, Admin/...)
- `wwwroot/` — css, js, images (cover images)
- `Services/` — PaymentService (Stripe), EmailService (اختیاری)
- `Areas/Identity` — Identity UI (در صورت استفاده)

---

## 🛠️ پیش‌نیاز و نصب
- .NET SDK (مطابق پروژه)
- SQL Server یا LocalDB
- Visual Studio یا VS Code
- حساب و کلیدهای Stripe برای پرداخت (در حالت عملی)

### نصب اولیه
```bash
git clone https://github.com/AdeptMehdi/BookStore.git
cd BookStore
dotnet restore
```
تنظیم `appsettings.json` (نمونه):
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=BookStoreDB;Trusted_Connection=True;MultipleActiveResultSets=true"
},
"Stripe": {
  "SecretKey": "...",
  "PublishableKey": "..."
}
```

### مایگریشن و دیتابیس
```powershell
Add-Migration InitialCreate
Update-Database
```

---

## 🔧 موارد کلیدی پیاده‌سازی (خلاصه)
- **BookModel**: Title, Description, Author, Price, Quantity, CoverImage (string), CategoryId, ICollection<ChapterModel>
- **ChapterModel**: Title, Content, BookId
- **Cart**: ذخیره در Session یا DB (برای کاربران لاگین‌شده)
- **Checkout**: ساخت Order و OrderItems، سپس فراخوانی Stripe Checkout یا PaymentIntent
- **Admin**: صفحات CRUD برای کتاب، دسته، سفارشات، مدیریت وضعیت سفارش
- **Session Extension**: متدهای کمکی برای ذخیره آبجکت در Session (JSON serialize)

---

## ✅ جریان کار (User flow)
1. کاربر صفحه اصلی را می‌بیند و کتاب‌ها را مرور می‌کند.  
2. روی View Details کلیک می‌کند، سرفصل‌ها را می‌بیند و به سبد اضافه می‌کند.  
3. سبد را بازبینی، تعداد را تغییر و پرداخت را شروع می‌کند.  
4. با Stripe پرداخت انجام می‌شود و پس از موفقیت Order ساخته می‌شود.  
5. ادمین در پنل سفارش‌ها را می‌بیند و وضعیت را به Shipped/Completed تغییر می‌دهد.

---

## 📌 نکات مفید
- برای تست Stripe از حالت تست (test keys) استفاده کنید.  
- برای تصاویر کاور در ابتدا از URL استفاده کنید تا روند سریع‌تر پیش برود، سپس در نسخه تولیدی آپلود را فعال کنید.  
- قبل از هر `Update-Database` حتما مایگریشن جدید بسازید.

---

## 📄 لایسنس و مشارکت
این پروژه مثال است و می‌توانید آن را توسعه داده و تحت **MIT** منتشر کنید. Pull request و issue خوش‌آمدند.

---

# 🛒 BookStore - README

**A simple responsive Book Store built with ASP.NET Core MVC and EF Core.** Customer-facing shopping flow, admin panel, orders, and Stripe payment integration (concept/sample).

---

## 🚀 Project Summary (English)
BookStore is a minimal e-commerce demo for books with features:
- Product listing (cover image, title, price, details)
- Book detail page: description, chapters, add-to-cart
- Shopping cart with quantity update and total calculation
- Checkout flow: shipping form, order summary, Stripe Checkout integration
- Orders: create order, list user orders, order status
- Authentication: ASP.NET Identity (Customer/Admin roles)
- Admin panel: manage books, categories, orders, users
- Interactive notifications with SweetAlert / Awesome Notifications

---

## 📦 Suggested Structure
- `Models/` — BookModel, ChapterModel, CategoryModel, OrderModel, OrderItemModel, ApplicationUser
- `Data/ApplicationDbContext.cs` — DbContext with DbSet properties
- `Controllers/` — HomeController, BookController, CartController, CheckoutController, OrderController, AdminController
- `Views/` — Razor views (Home, Book, Cart, Checkout, Admin/...)
- `wwwroot/` — static assets (css/js/images)
- `Services/` — PaymentService (Stripe), EmailService (optional)
- `Areas/Identity` — Identity UI (if used)

---

## 🛠️ Requirements & Setup
- .NET SDK (matching your project)
- SQL Server or LocalDB
- Visual Studio or VS Code
- Stripe account & keys (for real payments)

### Quick setup
```bash
git clone https://github.com/AdeptMehdi/BookStore.git
cd BookStore
dotnet restore
```
Sample `appsettings.json` entries:
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=.;Database=BookStoreDB;Trusted_Connection=True;MultipleActiveResultSets=true"
},
"Stripe": {
  "SecretKey": "...",
  "PublishableKey": "..."
}
```

### Migrations
```powershell
Add-Migration InitialCreate
Update-Database
```

---

## 🔧 Implementation Highlights
- **BookModel**: Title, Description, Author, Price, Quantity, CoverImage (string), CategoryId, ICollection<ChapterModel>
- **ChapterModel**: Title, Content, BookId
- **Cart**: stored in Session or DB (for authenticated users)
- **Checkout**: create Order + OrderItems, then call Stripe Checkout/PaymentIntent
- **Admin**: CRUD pages for books, categories, orders and change order status
- **Session Extension**: helper methods to store objects in session as JSON

---

## ✅ User Flow
1. Browse books on the homepage.  
2. Open Book Details, review chapters, add to cart.  
3. Edit cart items, proceed to checkout and enter shipping info.  
4. Pay with Stripe (test mode for development).  
5. Order is created; admin can update order status in panel.

---s

## 📌 Tips
- Use Stripe test keys during development.  
- Start with image URLs to speed development; add file upload later.  
- Always add migrations after model changes before updating DB.

---

## 📄 License & Contribution
This is a sample project. Feel free to adapt and publish under **MIT**. PRs and issues welcome.