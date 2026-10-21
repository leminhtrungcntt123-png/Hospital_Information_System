-- Nạp database + dữ liệu mẫu cho HIS WinForms bằng một lệnh.
-- Chạy từ thư mục gốc của solution:
--   psql -h localhost -p 5432 -U postgres -d postgres -v db_name=hospital_his -f src/HIS.DAL/Seed/setup_database.sql
-- Có thể bỏ -v db_name=... để dùng mặc định hospital_his.
-- Script an toàn khi chạy lặp lại: bảng dùng IF NOT EXISTS, dữ liệu dùng ON CONFLICT.

\set ON_ERROR_STOP on

\if :{?db_name}
\else
    \set db_name hospital_his
\endif

SELECT format('CREATE DATABASE %I', :'db_name')
WHERE NOT EXISTS (SELECT 1 FROM pg_database WHERE datname = :'db_name')
\gexec

\connect :db_name

-- Chặn trường hợp database có bảng cũ nhưng thiếu lịch sử migration:
-- schema.sql sẽ không tạo lại bảng và dễ gây lỗi khó hiểu.
DO $guard$
BEGIN
    IF EXISTS (
            SELECT 1 FROM information_schema.tables
            WHERE table_schema = 'public' AND table_name = 'accounts')
       AND NOT EXISTS (
            SELECT 1 FROM information_schema.tables
            WHERE table_schema = 'public' AND table_name = '__EFMigrationsHistory') THEN
        RAISE EXCEPTION
            'Database "%" đã có bảng nhưng thiếu __EFMigrationsHistory. Hãy dùng database mới (đổi db_name) hoặc sao lưu rồi tạo lại database.',
            current_database();
    END IF;
END
$guard$;

\ir schema.sql
\ir reception_seed.sql
\ir auth_seed.sql

\echo '=== Đã nạp xong database và dữ liệu mẫu ==='
