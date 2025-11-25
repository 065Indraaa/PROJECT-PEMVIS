-- =====================================================================
-- UPDATE TTL FORMAT - Ubah tanggal_lahir dari DATE ke VARCHAR(100)
-- =====================================================================
-- File ini untuk yang SUDAH PUNYA database db_sekolah
-- Jika belum punya, langsung import: db_sekolah_with_login.sql
-- =====================================================================

USE db_sekolah;

-- Backup data siswa terlebih dahulu (opsional)
-- CREATE TABLE tb_siswa_backup AS SELECT * FROM tb_siswa;

-- Drop stored procedure lama
DROP PROCEDURE IF EXISTS sp_insert_siswa;

-- Ubah tipe data tanggal_lahir dari DATE ke VARCHAR(100)
ALTER TABLE tb_siswa 
MODIFY COLUMN tanggal_lahir VARCHAR(100) COMMENT 'Format: Kota, DD Bulan YYYY';

-- Buat ulang stored procedure dengan parameter VARCHAR
DELIMITER $$

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

-- Update existing data (jika ada) dari format DATE ke format string
-- Contoh: '2008-05-15' menjadi 'Unknown, 15 Mei 2008'
-- Jika ingin konversi otomatis, uncomment script berikut:

/*
UPDATE tb_siswa 
SET tanggal_lahir = CASE
    WHEN tanggal_lahir LIKE '____-__-__' THEN 
        CONCAT('Unknown, ', 
               DATE_FORMAT(STR_TO_DATE(tanggal_lahir, '%Y-%m-%d'), '%d %M %Y'))
    ELSE tanggal_lahir
END;
*/

-- =====================================================================
-- VERIFIKASI
-- =====================================================================
SELECT 'Update berhasil! Field tanggal_lahir sekarang VARCHAR(100)' AS status;

-- Cek struktur tabel
DESCRIBE tb_siswa;

-- Cek stored procedure
SHOW PROCEDURE STATUS WHERE Db = 'db_sekolah' AND Name = 'sp_insert_siswa';

-- =====================================================================
-- CONTOH PENGGUNAAN
-- =====================================================================
/*
-- Test insert siswa baru dengan format TTL baru
CALL sp_insert_siswa(
    'NIS999',                      -- NIS
    'Test Siswa',                  -- Nama Lengkap
    'Laki-Laki',                   -- Jenis Kelamin
    'Kediri, 09 Agustus 2006',     -- TTL (Format Baru!)
    1,                             -- ID Kelas
    'Jl. Test',                    -- Alamat
    'Bapak Test',                  -- Nama Ayah
    'Ibu Test'                     -- Nama Ibu
);

-- Cek hasilnya
SELECT * FROM tb_siswa WHERE nis = 'NIS999';
SELECT * FROM tb_users WHERE username = 'NIS999';
*/

-- =====================================================================
-- SELESAI!
-- =====================================================================
