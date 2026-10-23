/* =========================================================
   booking.js - JavaScript cho quy trình đặt lịch khám
   Mỗi khối bên dưới chỉ chạy khi trang hiện tại có phần tử tương ứng:
     Bước 1  #khoa-form      chọn chuyên khoa + tìm kiếm
     Bước 2  #ngay-form      chọn ngày, tải ca khám bằng fetch, chọn ca
     Bước 3  #nguoi-form     chọn hồ sơ + modal thêm hồ sơ
     Bước 4  #xacnhan-form   đồng ý + chống gửi nhiều lần
     Bước 5  [data-print]    lưu / in phiếu hẹn
     Chung   modal thông tin bác sĩ
   ========================================================= */
(function () {
    'use strict';

    const $ = (sel, root = document) => root.querySelector(sel);
    const $$ = (sel, root = document) => Array.from(root.querySelectorAll(sel));
    const refreshIcons = () => { if (window.lucide) window.lucide.createIcons(); };

    // Bỏ dấu tiếng Việt để tìm kiếm không phân biệt dấu: "tim mach" khớp "Tim mạch".
    const normalize = (s) => s.toLowerCase().normalize('NFD')
        .replace(/[̀-ͯ]/g, '').replace(/đ/g, 'd');

    // ------------------------------------------------------------------ Modal chung

    const showModal = (name) => {
        const modal = $('#' + name + '-modal');
        const scrim = $('#' + name + '-scrim');
        if (modal) modal.hidden = false;
        if (scrim) scrim.hidden = false;
    };

    const closeModals = () => {
        $$('.modal, .modal-scrim').forEach((el) => { el.hidden = true; });
    };

    const setText = (id, text) => { const el = $('#' + id); if (el) el.textContent = text; };

    const openDoctorModal = (d) => {
        setText('dm-avatar', d.initials);
        setText('dm-name', d.name);
        setText('dm-title', d.title);
        setText('dm-specialty', d.specialty);
        setText('dm-experience', d.experience);
        setText('dm-time', d.time);
        setText('dm-room', d.room);
        setText('dm-date', d.date);
        showModal('doctor');
    };

    document.addEventListener('click', (e) => {
        const doctorBtn = e.target.closest('[data-doctor-open]');
        if (doctorBtn) {
            try { openDoctorModal(JSON.parse(doctorBtn.dataset.doctorOpen)); } catch (err) { /* bỏ qua dữ liệu lỗi */ }
            return;
        }
        if (e.target.closest('[data-open-add-profile]')) {
            showModal('add-profile');
            const first = $('#add-profile-form input');
            if (first) first.focus();
            return;
        }
        if (e.target.closest('[data-modal-close]')) closeModals();

        if (e.target.closest('[data-print]')) window.print();
    });

    document.addEventListener('keydown', (e) => { if (e.key === 'Escape') closeModals(); });

    // ------------------------------------------------------------------ Bước 1: Chọn chuyên khoa

    const khoaForm = $('#khoa-form');
    if (khoaForm) {
        const hidden = $('#field-department');
        const next = $('#khoa-next');
        const label = $('#khoa-selected-name');
        const grid = $('#khoa-grid');
        const empty = $('#khoa-empty');
        const search = $('#khoa-search');

        grid.addEventListener('click', (e) => {
            const card = e.target.closest('.specialty-card');
            if (!card || card.disabled) return;
            $$('.specialty-card', grid).forEach((c) => c.classList.toggle('selected', c === card));
            hidden.value = card.dataset.id;
            label.textContent = card.dataset.name;
            next.disabled = false;
        });

        search.addEventListener('input', () => {
            const q = normalize(search.value.trim());
            let visible = 0;
            $$('.specialty-card', grid).forEach((c) => {
                const match = normalize(c.dataset.name).includes(q);
                c.hidden = !match;
                if (match) visible++;
            });
            empty.hidden = visible > 0;
        });

        // Nhấn Enter trong ô tìm kiếm không được gửi form.
        search.addEventListener('keydown', (e) => { if (e.key === 'Enter') e.preventDefault(); });
    }

    // ------------------------------------------------------------------ Bước 2: Chọn ngày và ca

    const ngayForm = $('#ngay-form');
    if (ngayForm) {
        const hiddenDate = $('#field-date');
        const hiddenShift = $('#field-shift');
        const next = $('#ngay-next');
        const box = $('#shift-container');
        const strip = $('#date-strip');
        let requestId = 0;

        const loadShifts = async (date) => {
            const myRequest = ++requestId;      // bỏ qua kết quả cũ nếu người dùng bấm ngày khác rất nhanh
            hiddenDate.value = date;
            hiddenShift.value = '';
            next.disabled = true;
            box.innerHTML = '<div class="empty-state"><p>Đang tải ca khám...</p></div>';

            try {
                const res = await fetch(box.dataset.url + '?date=' + encodeURIComponent(date),
                    { headers: { 'X-Requested-With': 'XMLHttpRequest' } });
                if (!res.ok) throw new Error('HTTP ' + res.status);
                const html = await res.text();
                if (myRequest !== requestId) return;
                box.innerHTML = html;
            } catch (err) {
                if (myRequest !== requestId) return;
                box.innerHTML =
                    '<div class="alert error" role="alert"><i data-lucide="circle-alert"></i>' +
                    '<div><strong>Không tải được lịch khám</strong>' +
                    '<p>Vui lòng kiểm tra kết nối và thử lại. Các lựa chọn của bạn vẫn được giữ.</p></div>' +
                    '<button type="button" class="btn btn-outline" data-retry>Thử lại</button></div>';
            }
            refreshIcons();
        };

        strip.addEventListener('click', (e) => {
            const btn = e.target.closest('button[data-date]');
            if (!btn || btn.disabled || btn.classList.contains('selected')) return;
            $$('button', strip).forEach((b) => b.classList.toggle('selected', b === btn));
            loadShifts(btn.dataset.date);
        });

        box.addEventListener('click', (e) => {
            if (e.target.closest('[data-retry]')) { loadShifts(hiddenDate.value); return; }

            const btn = e.target.closest('.shift-select');
            if (!btn || btn.disabled) return;
            const card = btn.closest('.shift-card');
            $$('.shift-card', box).forEach((c) => c.classList.toggle('selected', c === card));
            hiddenShift.value = card.dataset.shift;
            setText('sum-shift', card.dataset.label + ' · ' + card.dataset.time);
            setText('sum-doctor', card.dataset.doctor);
            next.disabled = false;
        });

        ngayForm.addEventListener('submit', (e) => { if (!hiddenShift.value) e.preventDefault(); });
    }

    // ------------------------------------------------------------------ Bước 3: Chọn người khám

    const nguoiForm = $('#nguoi-form');
    if (nguoiForm) {
        const hidden = $('#field-patient');
        const next = $('#nguoi-next');
        const label = $('#nguoi-selected-name');
        const warn = $('#unverified-alert');

        nguoiForm.addEventListener('click', (e) => {
            const card = e.target.closest('.profile-select');
            if (!card) return;
            $$('.profile-select', nguoiForm).forEach((c) => c.classList.toggle('selected', c === card));
            hidden.value = card.dataset.id;
            label.textContent = card.dataset.name;
            const verified = card.dataset.verified === 'true';
            warn.hidden = verified;          // hồ sơ chưa xác minh: hiện cảnh báo và khóa nút Tiếp tục
            next.disabled = !verified;
        });
    }

    // Form thêm hồ sơ mới (kiểm tra phía trình duyệt; server kiểm tra lại)
    const addForm = $('#add-profile-form');
    if (addForm) {
        const isValidDob = (value) => {
            const m = /^(\d{2})\/(\d{2})\/(\d{4})$/.exec(value.trim());
            if (!m) return false;
            const d = +m[1], mo = +m[2], y = +m[3];
            const date = new Date(y, mo - 1, d);
            return date.getFullYear() === y && date.getMonth() === mo - 1 && date.getDate() === d && date <= new Date();
        };

        const rules = {
            FullName: (v) => v.trim().length > 0,
            DateOfBirth: isValidDob,
            Phone: (v) => /^0\d{9,10}$/.test(v.replace(/\s+/g, ''))
        };

        addForm.addEventListener('submit', (e) => {
            let ok = true;
            Object.keys(rules).forEach((name) => {
                const input = addForm.elements[name];
                const wrapper = input.closest('label');
                const valid = rules[name](input.value);
                wrapper.classList.toggle('error', !valid);
                const msg = $('small', wrapper);
                if (msg) msg.hidden = valid;
                if (!valid) ok = false;
            });
            if (!ok) { e.preventDefault(); return; }
            $('#add-profile-submit').disabled = true;   // chống gửi nhiều lần
        });
    }

    // ------------------------------------------------------------------ Bước 4: Xác nhận

    const xacNhanForm = $('#xacnhan-form');
    if (xacNhanForm) {
        const agree = $('#agree');
        const submit = $('#xacnhan-submit');
        let sending = false;

        agree.addEventListener('change', () => { submit.disabled = !agree.checked; });

        xacNhanForm.addEventListener('submit', (e) => {
            if (sending || !agree.checked) { e.preventDefault(); return; }   // chặn gửi nhiều lần
            sending = true;
            submit.disabled = true;
            submit.innerHTML = '<span class="spinner"></span> Đang gửi yêu cầu...';
        });
    }
})();