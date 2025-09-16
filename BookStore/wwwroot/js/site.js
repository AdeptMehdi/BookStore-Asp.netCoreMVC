// اجرای بعد از لود کامل صفحه
document.addEventListener("DOMContentLoaded", function () {
    // SweetAlert2 از TempData روی body
    const successMsg = document.body.getAttribute("data-success");
    const errorMsg = document.body.getAttribute("data-error");

    if (successMsg) {
        Swal.fire({ icon: 'success', title: 'موفق!', text: successMsg, confirmButtonColor: '#0ea5e9' });
    }
    if (errorMsg) {
        Swal.fire({ icon: 'error', title: 'خطا!', text: errorMsg, confirmButtonColor: '#ef4444' });
    }

    // نمایش/مخفی پسورد
    document.querySelectorAll(".toggle-pass").forEach(btn => {
        btn.addEventListener("click", function () {
            const input = this.parentElement.querySelector("input[type='password'], input[type='text']");
            if (!input) return;
            if (input.type === "password") {
                input.type = "text";
                this.innerHTML = '<i class="fa-solid fa-eye-slash"></i>';
            } else {
                input.type = "password";
                this.innerHTML = '<i class="fa-solid fa-eye"></i>';
            }
        });
    });
});

    // ===== انیمیشن کلیک روی دکمه‌ها =====
    document.querySelectorAll("button, .btn").forEach(btn => {
        btn.addEventListener("click", function () {
            this.classList.add("btn-clicked");
            setTimeout(() => this.classList.remove("btn-clicked"), 150);
        });
    });

});

