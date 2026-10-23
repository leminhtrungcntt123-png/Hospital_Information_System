-- Dữ liệu test cho chức năng Tiếp đón (chạy lặp lại an toàn).
-- Cách chạy:
--   psql -h localhost -p 5432 -U postgres -d hospital_his -f reception_seed.sql

\set ON_ERROR_STOP on
BEGIN;
SET client_encoding = 'UTF8';

-- 1. Danh mục vai trò
INSERT INTO roles (id, code, name, description) OVERRIDING SYSTEM VALUE VALUES
    (1, 'ADMIN',        'Quản trị hệ thống', 'Quản trị toàn bộ hệ thống'),
    (2, 'DOCTOR',       'Bác sĩ',            'Bác sĩ khám và điều trị'),
    (3, 'RECEPTIONIST', 'Tiếp đón',          'Nhân viên tiếp đón bệnh nhân'),
    (4, 'PATIENT',      'Bệnh nhân',         'Tài khoản cổng bệnh nhân')
ON CONFLICT (code) DO NOTHING;

SELECT setval(
    pg_get_serial_sequence('roles', 'id'),
    GREATEST((SELECT COALESCE(MAX(id), 1) FROM roles), 1)
);

-- 2. Danh mục trạng thái lượt khám
INSERT INTO appointment_statuses (id, code, name, sort_order, is_terminal) OVERRIDING SYSTEM VALUE VALUES
    (1, 'PENDING',     'Chờ xác nhận', 1, false),
    (2, 'CONFIRMED',   'Đã xác nhận',  2, false),
    (3, 'CHECKED_IN',  'Đã tiếp nhận', 3, false),
    (4, 'IN_PROGRESS', 'Đang khám',    4, false),
    (5, 'COMPLETED',   'Đã hoàn tất',  5, true),
    (6, 'CANCELLED',   'Đã hủy',       6, true)
ON CONFLICT (code) DO NOTHING;

SELECT setval(
    pg_get_serial_sequence('appointment_statuses', 'id'),
    GREATEST((SELECT COALESCE(MAX(id), 1) FROM appointment_statuses), 1)
);

-- 3. Khoa/phòng khám
INSERT INTO departments (name, description)
SELECT v.name, v.description
FROM (VALUES
    ('Khoa Tim mạch',      'Chẩn đoán và điều trị các bệnh lý tim mạch'),
    ('Khoa Nội tổng hợp',  'Khám và điều trị các bệnh nội khoa'),
    ('Khoa Nhi',           'Khám và điều trị bệnh lý trẻ em'),
    ('Khoa Ngoại',         'Khám và điều trị các bệnh lý ngoại khoa'),
    ('Khoa Sản',           'Khám thai và chăm sóc sức khỏe phụ nữ')
) AS v(name, description)
WHERE NOT EXISTS (SELECT 1 FROM departments d WHERE d.name = v.name);

-- 4. Phòng khám
INSERT INTO clinic_rooms (department_id, room_name, room_number)
SELECT d.id, v.room_name, v.room_number
FROM (VALUES
    ('Khoa Tim mạch',     'Phòng khám Tim mạch 101',     'P101'),
    ('Khoa Tim mạch',     'Phòng khám Tim mạch 102',     'P102'),
    ('Khoa Nội tổng hợp', 'Phòng khám Nội tổng hợp 201', 'P201'),
    ('Khoa Nhi',          'Phòng khám Nhi 301',          'P301'),
    ('Khoa Ngoại',        'Phòng khám Ngoại 401',        'P401'),
    ('Khoa Sản',          'Phòng khám Sản 501',          'P501')
) AS v(department_name, room_name, room_number)
JOIN departments d ON d.name = v.department_name
WHERE NOT EXISTS (
    SELECT 1 FROM clinic_rooms r
    WHERE r.department_id = d.id AND r.room_number = v.room_number
);

-- 5. Tài khoản nhân sự
INSERT INTO accounts (username, password_hash, role_id, is_active, created_at, updated_at)
SELECT v.username, 'TEST_ACCOUNT_NO_LOGIN', r.id, true, now(), now()
FROM (VALUES
    ('admin',           'ADMIN'),
    ('td.tranthivan',   'RECEPTIONIST')
) AS v(username, role_code)
JOIN roles r ON r.code = v.role_code
WHERE NOT EXISTS (SELECT 1 FROM accounts a WHERE a.username = v.username);

-- 6. Bác sĩ
INSERT INTO accounts (username, password_hash, role_id, is_active, created_at, updated_at)
SELECT v.username, 'TEST_ACCOUNT_NO_LOGIN', r.id, true, now(), now()
FROM (VALUES
    ('bs.tranthutrang'),
    ('bs.nguyenvanan'),
    ('bs.leminhchau'),
    ('bs.phamthuyduong'),
    ('bs.dovanhung'),
    ('bs.nguyenthilan')
) AS v(username)
CROSS JOIN roles r
WHERE r.code = 'DOCTOR'
  AND NOT EXISTS (SELECT 1 FROM accounts a WHERE a.username = v.username);

INSERT INTO doctors (account_id, department_id, full_name, specialty, license_number, avg_consultation_minutes)
SELECT a.id, d.id, v.full_name, v.specialty, v.license_number, 15
FROM (VALUES
    ('bs.tranthutrang',   'Khoa Tim mạch',     'BS.CKI Trần Thu Trang',   'Tim mạch',             'CKI-2020-001'),
    ('bs.nguyenvanan',    'Khoa Tim mạch',     'BS.CKII Nguyễn Văn An',   'Tim mạch can thiệp',   'CKII-2015-002'),
    ('bs.leminhchau',     'Khoa Nội tổng hợp', 'BS.CKI Lê Minh Châu',     'Nội tổng hợp',         'CKI-2018-003'),
    ('bs.phamthuyduong',  'Khoa Nhi',          'BS.CKI Phạm Thùy Dương',  'Nhi khoa',             'CKI-2019-004'),
    ('bs.dovanhung',      'Khoa Ngoại',        'BS.CKII Đỗ Văn Hùng',     'Ngoại tổng quát',      'CKII-2014-005'),
    ('bs.nguyenthilan',   'Khoa Sản',          'BS.CKI Nguyễn Thị Lan',   'Sản phụ khoa',         'CKI-2017-006')
) AS v(username, department_name, full_name, specialty, license_number)
JOIN accounts a ON a.username = v.username
JOIN departments d ON d.name = v.department_name
WHERE NOT EXISTS (SELECT 1 FROM doctors x WHERE x.license_number = v.license_number);

-- 7. Bệnh nhân test
INSERT INTO accounts (username, password_hash, role_id, is_active, created_at, updated_at)
SELECT v.username, 'TEST_ACCOUNT_NO_LOGIN', r.id, true, now(), now()
FROM (VALUES
    ('bn0001'), ('bn0002'), ('bn0003'), ('bn0004'), ('bn0005'), ('bn0006'),
    ('bn0007'), ('bn0008'), ('bn0009'), ('bn0010'), ('bn0011'), ('bn0012'),
    ('bn0013'), ('bn0014'), ('bn0015'), ('bn0016'), ('bn0017'), ('bn0018'),
    ('bn0019'), ('bn0020'), ('bn0021'), ('bn0022'), ('bn0023'), ('bn0024')
) AS v(username)
CROSS JOIN roles r
WHERE r.code = 'PATIENT'
  AND NOT EXISTS (SELECT 1 FROM accounts a WHERE a.username = v.username);

INSERT INTO patients (account_id, full_name, date_of_birth, gender, national_id, phone_number, address, insurance_card_number, created_at)
SELECT a.id, v.full_name, v.date_of_birth::date, v.gender, v.national_id, v.phone_number, v.address, v.insurance_card_number, now()
FROM (VALUES
    ('bn0001', 'Nguyễn Minh Anh',  '1990-04-12', 'FEMALE', '001190012345', '0912345678', '12 Nguyễn Huệ, Quận 1, TP. Hồ Chí Minh',   'DN4010123456789'),
    ('bn0002', 'Trần Thu Hà',      '1985-09-23', 'FEMALE', '001185009876', '0987654321', '45 Lê Lợi, Quận 1, TP. Hồ Chí Minh',       'DN4010987654321'),
    ('bn0003', 'Lê Hoàng Nam',     '1978-01-05', 'MALE',   '001178001234', '0905123456', '88 Hai Bà Trưng, Quận 3, TP. Hồ Chí Minh', 'DN4010012345678'),
    ('bn0004', 'Phạm Ngọc Lan',    '2018-06-17', 'FEMALE', '001218006789', '0938111222', '23 Võ Văn Tần, Quận 3, TP. Hồ Chí Minh',   'TE4010067890123'),
    ('bn0005', 'Vũ Đức Long',      '1965-11-30', 'MALE',   '001165011223', '0903777888', '156 Cách Mạng Tháng 8, Quận 10, TP. Hồ Chí Minh', 'DN4010112233344'),
    ('bn0006', 'Đỗ Thanh Bình',    '1992-02-14', 'MALE',   '001192002345', '0918222333', '7 Phan Xích Long, Phú Nhuận, TP. Hồ Chí Minh', 'DN4010023455667'),
    ('bn0007', 'Bùi Thị Mai',      '1996-07-08', 'FEMALE', '001196007890', '0945666777', '89 Nguyễn Trãi, Quận 5, TP. Hồ Chí Minh',  'DN4010078901234'),
    ('bn0008', 'Hoàng Văn Tú',     '1988-03-19', 'MALE',   '001188008901', '0977444555', '31 Điện Biên Phủ, Bình Thạnh, TP. Hồ Chí Minh', 'DN4010089012345'),
    ('bn0009', 'Ngô Thị Hồng',     '1994-12-02', 'FEMALE', '001194009012', '0966333444', '12 Nguyễn Kiệm, Gò Vấp, TP. Hồ Chí Minh',  'DN4010090123456'),
    ('bn0010', 'Đặng Quang Huy',   '2015-05-21', 'MALE',   '001215010123', '0909555666', '78 Quang Trung, Gò Vấp, TP. Hồ Chí Minh',  'TE4010101234567'),
    ('bn0011', 'Trịnh Thị Hương',  '1999-08-09', 'FEMALE', '001199011234', '0938222444', '5 Lý Thường Kiệt, Tân Bình, TP. Hồ Chí Minh', 'DN4010112345678'),
    ('bn0012', 'Phan Văn Khải',    '1970-10-10', 'MALE',   '001170012345', '0913666777', '210 Trường Chinh, Tân Phú, TP. Hồ Chí Minh', 'DN4010123456780'),
    ('bn0013', 'Lý Thị Kim',       '2001-01-25', 'FEMALE', '001201013456', '0942888999', '16 Hoàng Diệu, Quận 4, TP. Hồ Chí Minh',   'DN4010134567891'),
    ('bn0014', 'Cao Minh Quân',    '1983-04-03', 'MALE',   '001183014567', '0909333222', '102 Nguyễn Văn Linh, Quận 7, TP. Hồ Chí Minh', 'DN4010145678902'),
    ('bn0015', 'Nguyễn Thị Bích',  '1975-11-11', 'FEMALE', '001175015678', '0977111999', '33 Pasteur, Quận 1, TP. Hồ Chí Minh',      'DN4010156789013'),
    ('bn0016', 'Trương Gia Bảo',   '2019-09-05', 'MALE',   '001219016789', '0912555888', '55 Dương Quảng Hàm, Gò Vấp, TP. Hồ Chí Minh', 'TE4010167890124'),
    ('bn0017', 'Võ Thị Ánh',       '1987-02-28', 'FEMALE', '001187017890', '0908777666', '68 Trần Hưng Đạo, Quận 5, TP. Hồ Chí Minh', 'DN4010178901235'),
    ('bn0018', 'Đinh Công Thành',  '1993-06-15', 'MALE',   '001193018901', '0939111444', '141 Nguyễn Thị Minh Khai, Quận 1, TP. Hồ Chí Minh', 'DN4010189012346'),
    ('bn0019', 'Lâm Thanh Trúc',   '1997-03-07', 'FEMALE', '001197019012', '0968222111', '9 Ung Văn Khiêm, Bình Thạnh, TP. Hồ Chí Minh', 'DN4010190123457'),
    ('bn0020', 'Hồ Nhật Minh',     '2008-12-19', 'MALE',   '001208020123', '0903666555', '27 Nguyễn Xí, Bình Thạnh, TP. Hồ Chí Minh', 'DN4010201234568'),
    ('bn0021', 'Dương Thị Tuyết',  '1958-07-04', 'FEMALE', '001158021234', '0917222333', '302 Lê Văn Sỹ, Quận 3, TP. Hồ Chí Minh',   'DN4010212345679'),
    ('bn0022', 'Trần Quốc Cường',  '1981-05-12', 'MALE',   '001181022345', '0944777888', '19 Bạch Đằng, Tân Bình, TP. Hồ Chí Minh',  'DN4010223456780'),
    ('bn0023', 'Nguyễn Hải Yến',   '2003-10-22', 'FEMALE', '001203023456', '0909888777', '74 Phạm Văn Đồng, Thủ Đức, TP. Hồ Chí Minh', 'DN4010234567891'),
    ('bn0024', 'Bùi Xuân Phúc',    '1963-01-18', 'MALE',   '001163024567', '0914333222', '51 Đinh Tiên Hoàng, Bình Thạnh, TP. Hồ Chí Minh', 'DN4010245678902')
) AS v(username, full_name, date_of_birth, gender, national_id, phone_number, address, insurance_card_number)
JOIN accounts a ON a.username = v.username
WHERE NOT EXISTS (SELECT 1 FROM patients p WHERE p.national_id = v.national_id);

-- 8. Khung giờ khám cho 4 ngày (hôm nay và 3 ngày tới)
INSERT INTO appointment_slots (doctor_id, room_id, start_time, end_time, capacity, booked_count, status, version)
SELECT d.id, r.id,
       (CURRENT_DATE + s.day_offset + s.start_time)::timestamp,
       (CURRENT_DATE + s.day_offset + s.start_time + interval '30 minutes')::timestamp,
       4, 0, 'AVAILABLE', 0
FROM doctors d
JOIN clinic_rooms r ON r.department_id = d.department_id
    AND r.room_number = CASE d.license_number
        WHEN 'CKII-2015-002' THEN 'P102'
        WHEN 'CKI-2018-003'  THEN 'P201'
        WHEN 'CKI-2019-004'  THEN 'P301'
        WHEN 'CKII-2014-005' THEN 'P401'
        WHEN 'CKI-2017-006'  THEN 'P501'
        ELSE 'P101'
    END
CROSS JOIN (VALUES
    (0, time '08:00'), (0, time '08:30'), (0, time '09:00'), (0, time '09:30'),
    (0, time '10:00'), (0, time '14:00'), (0, time '14:30'),
    (1, time '08:00'), (1, time '08:30'), (1, time '09:00'), (1, time '09:30'),
    (1, time '10:00'), (1, time '14:00'), (1, time '14:30'),
    (2, time '08:00'), (2, time '08:30'), (2, time '09:00'), (2, time '09:30'),
    (2, time '10:00'), (2, time '14:00'), (2, time '14:30'),
    (3, time '08:00'), (3, time '08:30'), (3, time '09:00'), (3, time '09:30'),
    (3, time '10:00'), (3, time '14:00'), (3, time '14:30')
) AS s(day_offset, start_time)
WHERE NOT EXISTS (
    SELECT 1 FROM appointment_slots x
    WHERE x.doctor_id = d.id
      AND x.start_time = (CURRENT_DATE + s.day_offset + s.start_time)::timestamp
);

-- 9. Lượt khám test: 11 chờ tiếp nhận, 9 đã tiếp nhận, 8 lịch hẹn, 4 đã hủy
WITH seed(seq, patient_national_id, doctor_license, day_offset, slot_time, status_code, booking_channel, priority_level, reason, queue_number) AS (
    VALUES
    (1,  '001190012345', 'CKI-2020-001', 0, time '08:00', 'CONFIRMED',  'WEB', 1, 'Đau tức ngực khi gắng sức', NULL),
    (2,  '001185009876', 'CKI-2018-003', 0, time '08:00', 'CONFIRMED',  'WEB', 0, 'Ho kéo dài, mệt mỏi', NULL),
    (3,  '001178001234', 'CKII-2015-002', 0, time '08:30', 'PENDING',   'WEB', 0, 'Tái khám tăng huyết áp', NULL),
    (4,  '001218006789', 'CKI-2019-004', 0, time '08:30', 'CONFIRMED',  'WEB', 2, 'Sốt cao, quấy khóc', NULL),
    (5,  '001165011223', 'CKII-2014-005', 0, time '09:00', 'CONFIRMED',  'WEB', 1, 'Đau khớp gối', NULL),
    (6,  '001199011234', 'CKI-2017-006', 0, time '09:00', 'PENDING',    'WEB', 0, 'Khám thai định kỳ', NULL),
    (7,  '001170012345', 'CKI-2018-003', 0, time '09:30', 'CONFIRMED',  'WEB', 1, 'Đau dạ dày', NULL),
    (8,  '001201013456', 'CKI-2019-004', 0, time '09:30', 'PENDING',    'WEB', 0, 'Tái khám viêm họng', NULL),
    (9,  '001183014567', 'CKII-2014-005', 0, time '10:00', 'CONFIRMED',  'WEB', 0, 'Đau lưng sau vận động', NULL),
    (10, '001175015678', 'CKI-2020-001', 0, time '10:00', 'PENDING',    'WEB', 1, 'Hồi hộp, khó thở', NULL),
    (11, '001219016789', 'CKI-2019-004', 0, time '14:00', 'CONFIRMED',  'WEB', 2, 'Khám sức khỏe định kỳ', NULL),
    (12, '001192002345', 'CKI-2020-001', 0, time '08:00', 'CHECKED_IN', 'WEB', 0, 'Đau ngực trái', 'A001'),
    (13, '001196007890', 'CKI-2018-003', 0, time '08:30', 'CHECKED_IN', 'WEB', 0, 'Mệt mỏi kéo dài', 'A002'),
    (14, '001188008901', 'CKII-2015-002', 0, time '09:00', 'IN_PROGRESS','WEB', 1, 'Khó thở khi nằm', 'A003'),
    (15, '001194009012', 'CKI-2017-006', 0, time '09:00', 'CHECKED_IN', 'WEB', 0, 'Đau bụng dưới', 'A004'),
    (16, '001215010123', 'CKI-2019-004', 0, time '09:30', 'COMPLETED',  'WEB', 2, 'Sốt phát ban', 'A005'),
    (17, '001187017890', 'CKI-2018-003', 0, time '10:00', 'CHECKED_IN', 'WEB', 0, 'Đau đầu chóng mặt', 'A006'),
    (18, '001193018901', 'CKII-2014-005', 0, time '14:00', 'COMPLETED',  'WEB', 0, 'Đau vai gáy', 'A007'),
    (19, '001197019012', 'CKI-2017-006', 0, time '14:00', 'IN_PROGRESS','WEB', 0, 'Tái khám phụ khoa', 'A008'),
    (20, '001208020123', 'CKI-2019-004', 0, time '14:30', 'COMPLETED',  'WEB', 0, 'Khám nội tổng quát', 'A009'),
    (21, '001158021234', 'CKI-2020-001', 1, time '08:00', 'CONFIRMED',  'WEB', 1, 'Tái khám tim mạch', NULL),
    (22, '001181022345', 'CKI-2018-003', 1, time '08:30', 'CONFIRMED',  'WEB', 0, 'Kiểm tra sức khỏe', NULL),
    (23, '001203023456', 'CKI-2019-004', 1, time '09:00', 'PENDING',    'WEB', 0, 'Tư vấn dinh dưỡng', NULL),
    (24, '001163024567', 'CKII-2014-005', 1, time '09:30', 'CONFIRMED',  'WEB', 1, 'Đau thần kinh tọa', NULL),
    (25, '001190012345', 'CKI-2017-006', 2, time '08:00', 'CONFIRMED',  'WEB', 0, 'Khám thai lần 2', NULL),
    (26, '001192002345', 'CKII-2015-002', 2, time '08:30', 'PENDING',    'WEB', 0, 'Tư vấn tim mạch', NULL),
    (27, '001194009012', 'CKI-2018-003', 2, time '09:00', 'CONFIRMED',  'WEB', 0, 'Tái khám tiêu hóa', NULL),
    (28, '001219016789', 'CKI-2019-004', 3, time '08:00', 'CONFIRMED',  'WEB', 0, 'Khám sức khỏe trẻ em', NULL),
    (29, '001178001234', 'CKI-2018-003', 0, time '09:30', 'CANCELLED',  'WEB', 0, 'Đau họng', NULL),
    (30, '001188008901', 'CKI-2020-001', 0, time '14:30', 'CANCELLED',  'WEB', 0, 'Tái khám tim mạch', NULL),
    (31, '001201013456', 'CKII-2014-005', 0, time '14:00', 'CANCELLED',  'WEB', 0, 'Đau cổ tay', NULL),
    (32, '001208020123', 'CKI-2017-006', 0, time '14:30', 'CANCELLED',  'WEB', 0, 'Khám phụ khoa', NULL)
),
prepared AS (
    SELECT *,
           'LK' || to_char(CURRENT_DATE + day_offset, 'YYMMDD') || lpad(seq::text, 3, '0') AS appointment_code,
           (CURRENT_DATE + day_offset + slot_time)::timestamp AS slot_start
    FROM seed
)
INSERT INTO appointments (
    appointment_code, patient_id, slot_id, room_id, status_id, booking_channel, visit_type,
    priority_level, queue_number, reason, created_at, updated_at)
SELECT p.appointment_code, pat.id, s.id, s.room_id, st.id, p.booking_channel, 'SCHEDULED',
       p.priority_level, p.queue_number, p.reason,
       LEAST(now() - interval '30 minutes', p.slot_start - interval '1 day'),
       LEAST(now() - interval '10 minutes', p.slot_start - interval '1 day' + interval '5 minutes')
FROM prepared p
JOIN patients pat ON pat.national_id = p.patient_national_id
JOIN doctors d ON d.license_number = p.doctor_license
JOIN appointment_slots s ON s.doctor_id = d.id AND s.start_time = p.slot_start
JOIN appointment_statuses st ON st.code = p.status_code
WHERE NOT EXISTS (SELECT 1 FROM appointments a WHERE a.appointment_code = p.appointment_code);

-- 10. Lịch sử trạng thái cho mọi lượt khám chưa có lịch sử
WITH chains(current_code, step, old_code, new_code) AS (
    VALUES
    ('PENDING',     1, NULL::text,   'PENDING'),
    ('CONFIRMED',   1, NULL::text,   'CONFIRMED'),
    ('CHECKED_IN',  1, NULL::text,   'CONFIRMED'),
    ('CHECKED_IN',  2, 'CONFIRMED',  'CHECKED_IN'),
    ('IN_PROGRESS', 1, NULL::text,   'CONFIRMED'),
    ('IN_PROGRESS', 2, 'CONFIRMED',  'CHECKED_IN'),
    ('IN_PROGRESS', 3, 'CHECKED_IN', 'IN_PROGRESS'),
    ('COMPLETED',   1, NULL::text,   'CONFIRMED'),
    ('COMPLETED',   2, 'CONFIRMED',  'CHECKED_IN'),
    ('COMPLETED',   3, 'CHECKED_IN', 'IN_PROGRESS'),
    ('COMPLETED',   4, 'IN_PROGRESS','COMPLETED'),
    ('CANCELLED',   1, NULL::text,   'PENDING'),
    ('CANCELLED',   2, 'PENDING',    'CANCELLED')
)
INSERT INTO appointment_status_history (appointment_id, old_status_id, new_status_id, changed_at)
SELECT a.id, os.id, ns.id, a.created_at + (c.step * interval '2 minutes')
FROM appointments a
JOIN appointment_statuses current_status ON current_status.id = a.status_id
JOIN chains c ON c.current_code = current_status.code
LEFT JOIN appointment_statuses os ON os.code = c.old_code
JOIN appointment_statuses ns ON ns.code = c.new_code
WHERE NOT EXISTS (
    SELECT 1 FROM appointment_status_history h WHERE h.appointment_id = a.id
)
ORDER BY a.id, c.step;

-- 11. Cập nhật số chỗ đã đặt của khung giờ
UPDATE appointment_slots s
SET booked_count = x.booked,
    status = CASE WHEN x.booked >= s.capacity THEN 'FULL' ELSE 'AVAILABLE' END
FROM (
    SELECT slot_id, COUNT(*)::int AS booked
    FROM appointments
    WHERE slot_id IS NOT NULL
    GROUP BY slot_id
) x
WHERE s.id = x.slot_id;

COMMIT;

-- Kiểm tra nhanh sau khi seed
SELECT 'patients' AS entity, COUNT(*) AS total FROM patients
UNION ALL SELECT 'doctors', COUNT(*) FROM doctors
UNION ALL SELECT 'slots', COUNT(*) FROM appointment_slots
UNION ALL SELECT 'appointments', COUNT(*) FROM appointments
UNION ALL SELECT 'history', COUNT(*) FROM appointment_status_history;
