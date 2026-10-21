-- =====================================================
-- HIS - Mật khẩu demo cho các tài khoản đã seed
-- Mật khẩu lưu dạng BCrypt ($2a$, cost 11).
--
-- Chạy SAU reception_seed.sql:
--   psql -h localhost -p 5432 -U postgres -d hospital_his -f src/HIS.DAL/Seed/auth_seed.sql
--
-- Tài khoản demo:
--   admin          / Admin@123     (ADMIN)
--   td.tranthivan  / TiepDon@123   (RECEPTIONIST)
--   bs.*           / BacSi@123     (DOCTOR)
--   bn*            / BenhNhan@123  (PATIENT - không đăng nhập được app quản trị)
--
-- Script chạy lặp lại an toàn: mỗi lần chạy sẽ đặt lại mật khẩu,
-- mở khóa và bật lại các tài khoản demo.
-- =====================================================
\set ON_ERROR_STOP on
BEGIN;
SET client_encoding = 'UTF8';

-- ADMIN
UPDATE accounts
SET password_hash     = '$2a$11$qMqtae.vO2dI2pOSdCgyFucCRkZ3D1WW1P654bVZFjSAE.dhIW06i',
    failed_login_count = 0,
    locked_until      = NULL,
    is_active         = TRUE,
    updated_at        = now()
WHERE username = 'admin';

-- RECEPTIONIST
UPDATE accounts a
SET password_hash     = '$2a$11$oq4yHHLGHUY5cFNbzyu1iOBLfunDD61ig7Wux0dZkZNhM6aVfh9La',
    failed_login_count = 0,
    locked_until      = NULL,
    is_active         = TRUE,
    updated_at        = now()
FROM roles r
WHERE a.role_id = r.id
  AND r.code = 'RECEPTIONIST';

-- DOCTOR
UPDATE accounts a
SET password_hash     = '$2a$11$IX9qs7nXKSMrGTevzpVU4OVC3x7UTvPUVkt2oQCuoatXksMt/Ejaa',
    failed_login_count = 0,
    locked_until      = NULL,
    is_active         = TRUE,
    updated_at        = now()
FROM roles r
WHERE a.role_id = r.id
  AND r.code = 'DOCTOR';

-- PATIENT (chỉ dùng cho cổng web, ứng dụng quản trị sẽ từ chối)
UPDATE accounts a
SET password_hash     = '$2a$11$zKF.XHNqd1EqEDai1F3BlOXmMy08IRkH/GhFbZ8/0p9aQmXXbX8CK',
    failed_login_count = 0,
    locked_until      = NULL,
    is_active         = TRUE,
    updated_at        = now()
FROM roles r
WHERE a.role_id = r.id
  AND r.code = 'PATIENT';

COMMIT;

-- Kiểm tra nhanh sau khi chạy
SELECT r.code AS vai_tro, COUNT(a.id) AS so_tai_khoan
FROM accounts a
JOIN roles r ON r.id = a.role_id
GROUP BY r.code
ORDER BY r.code;
