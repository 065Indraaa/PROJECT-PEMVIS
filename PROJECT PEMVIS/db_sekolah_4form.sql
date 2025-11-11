-- =====================================================================
-- DATABASE UNTUK 4 FORM UTAMA
-- Database: db_sekolah
-- Form: FormDataGuru, FormDataSiswa, DataKelas, FormDataMapel
-- =====================================================================

-- Hapus database lama dan buat baru
DROP DATABASE IF EXISTS db_sekolah;
CREATE DATABASE db_sekolah;
USE db_sekolah;

-- =====================================================================
-- TABEL 1: tb_guru (untuk FormDataGuru)
-- =====================================================================
CREATE TABLE tb_guru (
    id_guru INT(11) PRIMARY KEY AUTO_INCREMENT,
    nip VARCHAR(50) NOT NULL UNIQUE,
    nama_lengkap VARCHAR(100) NOT NULL,
    mata_pelajaran VARCHAR(100),
    jenis_kelamin ENUM('Laki-Laki', 'Perempuan'),
    alamat TEXT,
    no_telepon VARCHAR(20)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- Sample data guru
INSERT INTO tb_guru (nip, nama_lengkap, mata_pelajaran, jenis_kelamin, alamat, no_telepon) VALUES
('NIP001', 'Budi Santoso, S.Pd', 'Matematika', 'Laki-Laki', 'Jl. Merdeka No. 10, Jakarta', '081234567891'),
('NIP002', 'Siti Aminah, S.Pd', 'Bahasa Indonesia', 'Perempuan', 'Jl. Sudirman No. 25, Bandung', '081234567892');

-- =====================================================================
-- TABEL 2: tb_kelas (untuk DataKelas)
-- =====================================================================
CREATE TABLE tb_kelas (
    id_kelas INT(11) PRIMARY KEY AUTO_INCREMENT,
    nama_kelas VARCHAR(50) NOT NULL,
    wali_kelas VARCHAR(100),
    kapasitas INT(11) DEFAULT 30
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- Sample data kelas
INSERT INTO tb_kelas (nama_kelas, wali_kelas, kapasitas) VALUES
('X-1', 'Budi Santoso, S.Pd', 30),
('X-2', 'Siti Aminah, S.Pd', 32),
('XI-1', 'Budi Santoso, S.Pd', 28),
('XI-2', 'Siti Aminah, S.Pd', 30),
('XII-1', 'Budi Santoso, S.Pd', 25),
('XII-2', 'Siti Aminah, S.Pd', 27);

-- =====================================================================
-- TABEL 3: tb_siswa (untuk FormDataSiswa)
-- =====================================================================
CREATE TABLE tb_siswa (
    id_siswa INT(11) PRIMARY KEY AUTO_INCREMENT,
    nis VARCHAR(50) NOT NULL UNIQUE,
    nama_lengkap VARCHAR(100) NOT NULL,
    jenis_kelamin ENUM('Laki-Laki', 'Perempuan'),
    tanggal_lahir DATE,
    id_kelas INT(11),
    alamat TEXT,
    nama_ayah VARCHAR(100),
    nama_ibu VARCHAR(100),
    FOREIGN KEY (id_kelas) REFERENCES tb_kelas(id_kelas) ON DELETE SET NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- Sample data siswa
INSERT INTO tb_siswa (nis, nama_lengkap, jenis_kelamin, tanggal_lahir, id_kelas, alamat, nama_ayah, nama_ibu) VALUES
('NIS001', 'Ahmad Rizki', 'Laki-Laki', '2008-05-15', 1, 'Jl. Merdeka No. 1', 'Bapak Ahmad', 'Ibu Siti'),
('NIS002', 'Dewi Lestari', 'Perempuan', '2008-08-20', 1, 'Jl. Sudirman No. 2', 'Bapak Budi', 'Ibu Ani'),
('NIS003', 'Andi Pratama', 'Laki-Laki', '2008-03-10', 2, 'Jl. Gatot Subroto No. 3', 'Bapak Andi', 'Ibu Dewi');

-- =====================================================================
-- TABEL 4: tb_mata_pelajaran (untuk FormDataMapel)
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

-- Sample data mata pelajaran
INSERT INTO tb_mata_pelajaran (kode_mapel, nama_mapel, id_guru, semester, kelas) VALUES
('MTK', 'Matematika', 1, '1', 'X'),
('BIN', 'Bahasa Indonesia', 2, '1', 'X'),
('IPA', 'Ilmu Pengetahuan Alam', 1, '2', 'XI'),
('IPS', 'Ilmu Pengetahuan Sosial', 2, '2', 'XI');

-- =====================================================================
-- VIEW untuk mempermudah query JOIN
-- =====================================================================

-- View untuk FormDataSiswa (dengan nama kelas)
CREATE VIEW v_siswa_lengkap AS
SELECT 
    s.id_siswa,
    s.nis,
    s.nama_lengkap,
    s.jenis_kelamin,
    s.tanggal_lahir,
    k.nama_kelas,
    s.alamat,
    s.nama_ayah,
    s.nama_ibu
FROM tb_siswa s
LEFT JOIN tb_kelas k ON s.id_kelas = k.id_kelas;

-- View untuk FormDataMapel (dengan nama guru)
CREATE VIEW v_mapel_lengkap AS
SELECT 
    m.id_mapel,
    m.kode_mapel,
    m.nama_mapel,
    g.nama_lengkap AS nama_guru,
    m.semester,
    m.kelas
FROM tb_mata_pelajaran m
LEFT JOIN tb_guru g ON m.id_guru = g.id_guru;

-- =====================================================================
-- VERIFIKASI DATA
-- =====================================================================
SELECT '=== DATA GURU ===' AS Info;
SELECT * FROM tb_guru;

SELECT '=== DATA KELAS ===' AS Info;
SELECT * FROM tb_kelas;

SELECT '=== DATA SISWA ===' AS Info;
SELECT * FROM v_siswa_lengkap;

SELECT '=== DATA MATA PELAJARAN ===' AS Info;
SELECT * FROM v_mapel_lengkap;

SELECT '✅ DATABASE db_sekolah SIAP DIGUNAKAN!' AS Status;
