-- =====================================================================
-- DATABASE DENGAN SISTEM LOGIN & ROLE
-- Database: db_sekolah
-- =====================================================================

DROP DATABASE IF EXISTS db_sekolah;
CREATE DATABASE db_sekolah;
USE db_sekolah;

-- =====================================================================
-- TABEL 1: tb_role (Role User)
-- =====================================================================
CREATE TABLE tb_role (
    id_role INT(11) PRIMARY KEY AUTO_INCREMENT,
    nama_role VARCHAR(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

INSERT INTO tb_role (nama_role) VALUES
('Admin'),
('Guru'),
('Siswa');

-- =====================================================================
-- TABEL 2: tb_users (Login & Authentication)
-- =====================================================================
CREATE TABLE tb_users (
    id_user INT(11) PRIMARY KEY AUTO_INCREMENT,
    username VARCHAR(50) NOT NULL UNIQUE,
    password VARCHAR(100) NOT NULL,
    nama_lengkap VARCHAR(100) NOT NULL,
    id_role INT(11) NOT NULL,
    status ENUM('Aktif', 'Nonaktif') DEFAULT 'Aktif',
    FOREIGN KEY (id_role) REFERENCES tb_role(id_role)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- Sample admin
INSERT INTO tb_users (username, password, nama_lengkap, id_role, status) VALUES
('admin', 'admin123', 'Administrator', 1, 'Aktif');

-- =====================================================================
-- TABEL 3: tb_guru
-- =====================================================================
CREATE TABLE tb_guru (
    id_guru INT(11) PRIMARY KEY AUTO_INCREMENT,
    id_user INT(11) NOT NULL,
    nip VARCHAR(50) NOT NULL UNIQUE,
    nama_lengkap VARCHAR(100) NOT NULL,
    mata_pelajaran VARCHAR(100),
    jenis_kelamin ENUM('Laki-Laki', 'Perempuan'),
    alamat TEXT,
    no_telepon VARCHAR(20),
    FOREIGN KEY (id_user) REFERENCES tb_users(id_user) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- =====================================================================
-- TABEL 4: tb_kelas
-- =====================================================================
CREATE TABLE tb_kelas (
    id_kelas INT(11) PRIMARY KEY AUTO_INCREMENT,
    nama_kelas VARCHAR(50) NOT NULL,
    wali_kelas VARCHAR(100),
    kapasitas INT(11) DEFAULT 30
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

INSERT INTO tb_kelas (nama_kelas, wali_kelas, kapasitas) VALUES
('X-1', 'Budi Santoso, S.Pd', 30),
('X-2', 'Siti Aminah, S.Pd', 32),
('XI-1', 'Budi Santoso, S.Pd', 28),
('XI-2', 'Siti Aminah, S.Pd', 30),
('XII-1', 'Budi Santoso, S.Pd', 25),
('XII-2', 'Siti Aminah, S.Pd', 27);

-- =====================================================================
-- TABEL 5: tb_siswa
-- =====================================================================
CREATE TABLE tb_siswa (
    id_siswa INT(11) PRIMARY KEY AUTO_INCREMENT,
    id_user INT(11) NOT NULL,
    nis VARCHAR(50) NOT NULL UNIQUE,
    nama_lengkap VARCHAR(100) NOT NULL,
    jenis_kelamin ENUM('Laki-Laki', 'Perempuan'),
    tanggal_lahir VARCHAR(100),  -- Format: "Kediri, 09 Agustus 2006"
    id_kelas INT(11),
    alamat TEXT,
    nama_ayah VARCHAR(100),
    nama_ibu VARCHAR(100),
    FOREIGN KEY (id_user) REFERENCES tb_users(id_user) ON DELETE CASCADE,
    FOREIGN KEY (id_kelas) REFERENCES tb_kelas(id_kelas) ON DELETE SET NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- =====================================================================
-- TABEL 6: tb_mata_pelajaran
-- =====================================================================
CREATE TABLE tb_mata_pelajaran (
    id_mapel INT(11) PRIMARY KEY AUTO_INCREMENT,
    kode_mapel VARCHAR(20) NOT NULL UNIQUE,
    nama_mapel VARCHAR(100) NOT NULL,
    id_guru INT(11),
    semester ENUM('1', '2'),
    kelas VARCHAR(50),
    FOREIGN KEY (id_guru) REFERENCES tb_guru(id_guru) ON DELETE SET NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- =====================================================================
-- SAMPLE DATA GURU (dengan auto create user)
-- =====================================================================

-- User untuk Guru 1
INSERT INTO tb_users (username, password, nama_lengkap, id_role, status) VALUES
('NIP001', 'Budi', 'Budi Santoso, S.Pd', 2, 'Aktif');
INSERT INTO tb_guru (id_user, nip, nama_lengkap, mata_pelajaran, jenis_kelamin, alamat, no_telepon) VALUES
(LAST_INSERT_ID(), 'NIP001', 'Budi Santoso, S.Pd', 'Matematika', 'Laki-Laki', 'Jl. Merdeka No. 10, Jakarta', '081234567891');

-- User untuk Guru 2
INSERT INTO tb_users (username, password, nama_lengkap, id_role, status) VALUES
('NIP002', 'Siti', 'Siti Aminah, S.Pd', 2, 'Aktif');
INSERT INTO tb_guru (id_user, nip, nama_lengkap, mata_pelajaran, jenis_kelamin, alamat, no_telepon) VALUES
(LAST_INSERT_ID(), 'NIP002', 'Siti Aminah, S.Pd', 'Bahasa Indonesia', 'Perempuan', 'Jl. Sudirman No. 25, Bandung', '081234567892');

-- =====================================================================
-- SAMPLE DATA SISWA (dengan auto create user)
-- =====================================================================

-- User untuk Siswa 1
INSERT INTO tb_users (username, password, nama_lengkap, id_role, status) VALUES
('NIS001', 'Ahmad', 'Ahmad Rizki', 3, 'Aktif');
INSERT INTO tb_siswa (id_user, nis, nama_lengkap, jenis_kelamin, tanggal_lahir, id_kelas, alamat, nama_ayah, nama_ibu) VALUES
(LAST_INSERT_ID(), 'NIS001', 'Ahmad Rizki', 'Laki-Laki', 'Jakarta, 15 Mei 2008', 1, 'Jl. Merdeka No. 1', 'Bapak Ahmad', 'Ibu Siti');

-- User untuk Siswa 2
INSERT INTO tb_users (username, password, nama_lengkap, id_role, status) VALUES
('NIS002', 'Dewi', 'Dewi Lestari', 3, 'Aktif');
INSERT INTO tb_siswa (id_user, nis, nama_lengkap, jenis_kelamin, tanggal_lahir, id_kelas, alamat, nama_ayah, nama_ibu) VALUES
(LAST_INSERT_ID(), 'NIS002', 'Dewi Lestari', 'Perempuan', 'Bandung, 20 Agustus 2008', 1, 'Jl. Sudirman No. 2', 'Bapak Budi', 'Ibu Ani');

-- User untuk Siswa 3
INSERT INTO tb_users (username, password, nama_lengkap, id_role, status) VALUES
('NIS003', 'Andi', 'Andi Pratama', 3, 'Aktif');
INSERT INTO tb_siswa (id_user, nis, nama_lengkap, jenis_kelamin, tanggal_lahir, id_kelas, alamat, nama_ayah, nama_ibu) VALUES
(LAST_INSERT_ID(), 'NIS003', 'Andi Pratama', 'Laki-Laki', 'Surabaya, 10 Maret 2008', 2, 'Jl. Gatot Subroto No. 3', 'Bapak Andi', 'Ibu Dewi');

-- =====================================================================
-- SAMPLE DATA MATA PELAJARAN
-- =====================================================================
INSERT INTO tb_mata_pelajaran (kode_mapel, nama_mapel, id_guru, semester, kelas) VALUES
('MTK', 'Matematika', 1, '1', 'X'),
('BIN', 'Bahasa Indonesia', 2, '1', 'X'),
('IPA', 'Ilmu Pengetahuan Alam', 1, '2', 'XI'),
('IPS', 'Ilmu Pengetahuan Sosial', 2, '2', 'XI');

-- =====================================================================
-- VIEW untuk JOIN data
-- =====================================================================

-- View Guru dengan User
CREATE VIEW v_guru_lengkap AS
SELECT 
    g.id_guru, g.id_user, u.username, g.nip, g.nama_lengkap,
    g.mata_pelajaran, g.jenis_kelamin, g.alamat, g.no_telepon,
    u.status, r.nama_role
FROM tb_guru g
INNER JOIN tb_users u ON g.id_user = u.id_user
INNER JOIN tb_role r ON u.id_role = r.id_role;

-- View Siswa dengan User dan Kelas
CREATE VIEW v_siswa_lengkap AS
SELECT 
    s.id_siswa, s.id_user, u.username, s.nis, s.nama_lengkap,
    s.jenis_kelamin, s.tanggal_lahir, k.nama_kelas,
    s.alamat, s.nama_ayah, s.nama_ibu, u.status, r.nama_role
FROM tb_siswa s
INNER JOIN tb_users u ON s.id_user = u.id_user
INNER JOIN tb_role r ON u.id_role = r.id_role
LEFT JOIN tb_kelas k ON s.id_kelas = k.id_kelas;

-- View Mapel dengan Guru
CREATE VIEW v_mapel_lengkap AS
SELECT 
    m.id_mapel, m.kode_mapel, m.nama_mapel,
    g.nama_lengkap AS nama_guru, m.semester, m.kelas
FROM tb_mata_pelajaran m
LEFT JOIN tb_guru g ON m.id_guru = g.id_guru;

-- =====================================================================
-- STORED PROCEDURE untuk Auto Create User saat Insert Guru
-- =====================================================================
DELIMITER $$

CREATE PROCEDURE sp_insert_guru(
    IN p_nip VARCHAR(50),
    IN p_nama_lengkap VARCHAR(100),
    IN p_mata_pelajaran VARCHAR(100),
    IN p_jenis_kelamin VARCHAR(20),
    IN p_alamat TEXT,
    IN p_no_telepon VARCHAR(20)
)
BEGIN
    DECLARE v_id_user INT;
    DECLARE v_password VARCHAR(100);
    
    -- Ambil nama depan untuk password (atau bisa pakai nama lengkap)
    SET v_password = SUBSTRING_INDEX(p_nama_lengkap, ' ', 1);
    
    -- Insert ke tb_users
    INSERT INTO tb_users (username, password, nama_lengkap, id_role, status)
    VALUES (p_nip, v_password, p_nama_lengkap, 2, 'Aktif');
    
    SET v_id_user = LAST_INSERT_ID();
    
    -- Insert ke tb_guru
    INSERT INTO tb_guru (id_user, nip, nama_lengkap, mata_pelajaran, jenis_kelamin, alamat, no_telepon)
    VALUES (v_id_user, p_nip, p_nama_lengkap, p_mata_pelajaran, p_jenis_kelamin, p_alamat, p_no_telepon);
END$$

-- =====================================================================
-- STORED PROCEDURE untuk Auto Create User saat Insert Siswa
-- =====================================================================
CREATE PROCEDURE sp_insert_siswa(
    IN p_nis VARCHAR(50),
    IN p_nama_lengkap VARCHAR(100),
    IN p_jenis_kelamin VARCHAR(20),
    IN p_tanggal_lahir VARCHAR(100),  -- Format: "Kediri, 09 Agustus 2006"
    IN p_id_kelas INT,
    IN p_alamat TEXT,
    IN p_nama_ayah VARCHAR(100),
    IN p_nama_ibu VARCHAR(100)
)
BEGIN
    DECLARE v_id_user INT;
    DECLARE v_password VARCHAR(100);
    
    -- Ambil nama depan untuk password
    SET v_password = SUBSTRING_INDEX(p_nama_lengkap, ' ', 1);
    
    -- Insert ke tb_users
    INSERT INTO tb_users (username, password, nama_lengkap, id_role, status)
    VALUES (p_nis, v_password, p_nama_lengkap, 3, 'Aktif');
    
    SET v_id_user = LAST_INSERT_ID();
    
    -- Insert ke tb_siswa
    INSERT INTO tb_siswa (id_user, nis, nama_lengkap, jenis_kelamin, tanggal_lahir, id_kelas, alamat, nama_ayah, nama_ibu)
    VALUES (v_id_user, p_nis, p_nama_lengkap, p_jenis_kelamin, p_tanggal_lahir, p_id_kelas, p_alamat, p_nama_ayah, p_nama_ibu);
END$$

DELIMITER ;

-- =====================================================================
-- VERIFIKASI
-- =====================================================================
SELECT '========================================' AS '';
SELECT '✅ DATABASE WITH LOGIN SYSTEM' AS '';
SELECT '========================================' AS '';

SELECT 'LOGIN CREDENTIALS:' AS Info;
SELECT username, password, nama_lengkap, nama_role 
FROM tb_users u
INNER JOIN tb_role r ON u.id_role = r.id_role;

SELECT '' AS '';
SELECT '========================================' AS '';
SELECT 'DATA GURU:' AS '';
SELECT * FROM v_guru_lengkap;

SELECT '' AS '';
SELECT '========================================' AS '';
SELECT 'DATA SISWA:' AS '';
SELECT * FROM v_siswa_lengkap;

SELECT '' AS '';
SELECT '========================================' AS '';
SELECT '✅ DATABASE SIAP DIGUNAKAN!' AS '';
SELECT 'Username Guru = NIP, Password = Nama Depan' AS '';
SELECT 'Username Siswa = NIS, Password = Nama Depan' AS '';
SELECT '========================================' AS '';
