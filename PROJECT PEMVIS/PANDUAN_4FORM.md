# 📘 PANDUAN LENGKAP - 4 FORM UTAMA

## 🎯 FILE SQL: `db_sekolah_4form.sql`

### Cara Import:
**Via phpMyAdmin:**
1. Buka `http://localhost/phpmyadmin`
2. Klik tab "Import"
3. Pilih file: `db_sekolah_4form.sql`
4. Klik "Go"

**Via Command Line:**
```bash
mysql -u root < "d:\SEMESTER 3\Pemrograman visual\UTS\PROJECT PEMVIS\db_sekolah_4form.sql"
```

---

## 📊 STRUKTUR DATABASE

### Tabel 1: **tb_guru**
```sql
id_guru INT PRIMARY KEY AUTO_INCREMENT
nip VARCHAR(50) UNIQUE
nama_lengkap VARCHAR(100)
mata_pelajaran VARCHAR(100)
jenis_kelamin ENUM('Laki-Laki', 'Perempuan')
alamat TEXT
no_telepon VARCHAR(20)
```

### Tabel 2: **tb_kelas**
```sql
id_kelas INT PRIMARY KEY AUTO_INCREMENT
nama_kelas VARCHAR(50)
wali_kelas VARCHAR(100)
kapasitas INT
```

### Tabel 3: **tb_siswa**
```sql
id_siswa INT PRIMARY KEY AUTO_INCREMENT
nis VARCHAR(50) UNIQUE
nama_lengkap VARCHAR(100)
jenis_kelamin ENUM('Laki-Laki', 'Perempuan')
tanggal_lahir DATE
id_kelas INT (FK → tb_kelas)
alamat TEXT
nama_ayah VARCHAR(100)
nama_ibu VARCHAR(100)
```

### Tabel 4: **tb_mata_pelajaran**
```sql
id_mapel INT PRIMARY KEY AUTO_INCREMENT
kode_mapel VARCHAR(20) UNIQUE
nama_mapel VARCHAR(100)
id_guru INT (FK → tb_guru)
semester ENUM('1', '2')
kelas VARCHAR(50)
```

---

## 🔗 MAPPING: DataGridView ↔ Database

### 1. FormDataGuru
| DataGridView Column | Database Column | Input Control |
|-------------------|-----------------|---------------|
| KodeGuru (Col 0) | `nip` | TextBox1 |
| NamaGuru (Col 1) | `nama_lengkap` | TextBox2 |
| Mapel (Col 2) | `mata_pelajaran` | TextBox4 |
| JenisKelamin (Col 3) | `jenis_kelamin` | RadioButton1/2 |
| Alamat (Col 4) | `alamat` | TextBox3 |
| NoTlpn (Col 5) | `no_telepon` | TextBox5 |

**Query:**
```sql
SELECT nip, nama_lengkap, mata_pelajaran, jenis_kelamin, alamat, no_telepon 
FROM tb_guru ORDER BY id_guru
```

### 2. FormDataSiswa
| DataGridView Column | Database Column | Input Control |
|-------------------|-----------------|---------------|
| NIS (Col 0) | `nis` | TextBox1 |
| Nama (Col 1) | `nama_lengkap` | TextBox2 |
| JenisKelamin (Col 2) | `jenis_kelamin` | RadioButton1/2 |
| TTL (Col 3) | `tanggal_lahir` | TextBox3 |
| Kelas (Col 4) | `nama_kelas` (from tb_kelas) | TextBox4 |
| Alamat (Col 5) | `alamat` | TextBox5 |
| Ayah (Col 6) | `nama_ayah` | TextBox6 |
| Ibu (Col 7) | `nama_ibu` | TextBox7 |

**Query:**
```sql
SELECT nis, nama_lengkap, jenis_kelamin, tanggal_lahir, nama_kelas, alamat, nama_ayah, nama_ibu 
FROM v_siswa_lengkap
```

### 3. DataKelas
| DataGridView Column | Database Column | Input Control |
|-------------------|-----------------|---------------|
| Namakelas (Col 0) | `nama_kelas` | TextBox1 |
| Walikelas (Col 1) | `wali_kelas` | TextBox2 |
| Kapasitas (Col 2) | `kapasitas` | HScrollBar1 atau TextBox |

**Query:**
```sql
SELECT nama_kelas, wali_kelas, kapasitas 
FROM tb_kelas ORDER BY id_kelas
```

### 4. FormDataMapel
| DataGridView Column | Database Column | Input Control |
|-------------------|-----------------|---------------|
| KodePelajaran (Col 0) | `kode_mapel` | TextBox1 |
| NamaPelajaran (Col 1) | `nama_mapel` | TextBox2 |
| NamaGuru (Col 2) | `nama_guru` (from tb_guru) | ComboBox1 |
| Semester (Col 3) | `semester` | TextBox3 |
| Kelas (Col 4) | `kelas` | TextBox4 |

**Query:**
```sql
SELECT kode_mapel, nama_mapel, nama_guru, semester, kelas 
FROM v_mapel_lengkap
```

---

## ✅ STATUS IMPLEMENTASI

### FormDataGuru: ✅ SELESAI
- [x] FormDataGuru.vb - CRUD lengkap
- [x] FormDataGuru.Designer.vb - Event handlers
- [x] Database tb_guru
- [x] Fungsi: Simpan, Edit, Hapus, Cari, TampilData

### FormDataSiswa: ⏳ SIAP IMPLEMENTASI
- [ ] Perlu kode VB.NET
- [x] Database tb_siswa & v_siswa_lengkap sudah ready

### DataKelas: ⏳ SIAP IMPLEMENTASI
- [ ] Perlu kode VB.NET
- [x] Database tb_kelas sudah ready

### FormDataMapel: ⏳ SIAP IMPLEMENTASI
- [ ] Perlu kode VB.NET
- [x] Database tb_mata_pelajaran & v_mapel_lengkap sudah ready

---

## 🚀 CARA PENGGUNAAN

### FormDataGuru (Sudah Siap):

1. **Simpan Data Baru:**
   - Isi TextBox1 (NIP)
   - Isi TextBox2 (Nama)
   - Isi TextBox4 (Mapel)
   - Pilih RadioButton (Jenis Kelamin)
   - Isi TextBox3 (Alamat)
   - Isi TextBox5 (No Telp)
   - Klik Button1 (Simpan)

2. **Edit Data:**
   - Klik baris di DataGridView
   - Ubah data di form
   - Klik Button3 (Edit)

3. **Hapus Data:**
   - Klik baris di DataGridView
   - Klik Button4 (Hapus)
   - Konfirmasi Yes

4. **Cari Data:**
   - Ketik NIP atau Nama di TextBox8
   - Klik Button6 (Cari)

---

## 🔧 KONEKSI DATABASE

**Connection String:**
```vb
server=localhost;user=root;password=;database=db_sekolah
```

**Ubah sesuai setting MySQL Anda:**
- Jika pakai port 3306 (default): tidak perlu ubah
- Jika pakai port lain: tambahkan `;port=3307`
- Jika ada password: `password=yourpass`

---

## ⚠️ TROUBLESHOOTING

**Error: "Table doesn't exist"**
- Pastikan sudah import `db_sekolah_4form.sql`
- Cek database aktif: `SHOW DATABASES;`

**Error: "Connection failed"**
- Cek MySQL service running
- Cek username/password
- Cek port (default 3306)

**DataGridView kosong:**
- Cek koneksi database
- Cek query SELECT
- Cek nama kolom di DataGridView sama dengan database

---

## 📝 NEXT STEPS

Untuk melengkapi 3 form lainnya, ikuti pola yang sama dengan FormDataGuru:

1. Import MySql.Data.MySqlClient
2. Buat fungsi koneksi()
3. Buat fungsi TampilData()
4. Buat event handler untuk button CRUD
5. Tambahkan AddHandler di Designer.vb

**Contoh sudah ada di FormDataGuru.vb!** 🎉
