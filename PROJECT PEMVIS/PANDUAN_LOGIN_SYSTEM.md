# 🔐 PANDUAN SISTEM LOGIN & ROLE

## 📊 STRUKTUR DATABASE

### 1. **tb_role** (Role User)
```sql
id_role | nama_role
--------|----------
   1    | Admin
   2    | Guru
   3    | Siswa
```

### 2. **tb_users** (Authentication)
```sql
id_user | username | password      | nama_lengkap        | id_role | status
--------|----------|---------------|---------------------|---------|--------
   1    | admin    | admin123      | Administrator       |    1    | Aktif
   2    | NIP001   | Budi          | Budi Santoso, S.Pd  |    2    | Aktif
   3    | NIP002   | Siti          | Siti Aminah, S.Pd   |    2    | Aktif
   4    | NIS001   | Ahmad         | Ahmad Rizki         |    3    | Aktif
   5    | NIS002   | Dewi          | Dewi Lestari        |    3    | Aktif
```

### 3. **tb_guru** (Data Guru + id_user sebagai FK)
```sql
id_guru | id_user | nip    | nama_lengkap        | mata_pelajaran | ...
--------|---------|--------|---------------------|----------------|-----
   1    |    2    | NIP001 | Budi Santoso, S.Pd  | Matematika     | ...
   2    |    3    | NIP002 | Siti Aminah, S.Pd   | Bahasa Indo    | ...
```

### 4. **tb_siswa** (Data Siswa + id_user sebagai FK)
```sql
id_siswa | id_user | nis    | nama_lengkap  | id_kelas | ...
---------|---------|--------|---------------|----------|-----
   1     |    4    | NIS001 | Ahmad Rizki   |    1     | ...
   2     |    5    | NIS002 | Dewi Lestari  |    1     | ...
```

---

## 🔑 CREDENTIAL SYSTEM

### Format Login:

**Untuk Guru:**
- **Username:** NIP (contoh: `NIP001`)
- **Password:** Nama Depan (contoh: `Budi`)

**Untuk Siswa:**
- **Username:** NIS (contoh: `NIS001`)
- **Password:** Nama Depan (contoh: `Ahmad`)

**Untuk Admin:**
- **Username:** `admin`
- **Password:** `admin123`

---

## 🚀 CARA KERJA AUTO CREATE USER

### Saat Input Guru Baru di FormDataGuru:

1. **User mengisi form:**
   - TextBox1 (NIP): `NIP003`
   - TextBox2 (Nama): `John Doe, S.Pd`
   - TextBox4 (Mapel): `Fisika`
   - dst...

2. **Klik Button Simpan (Button1)**

3. **System otomatis:**
   ```sql
   -- Step 1: Insert ke tb_users
   INSERT INTO tb_users (username, password, nama_lengkap, id_role, status)
   VALUES ('NIP003', 'John', 'John Doe, S.Pd', 2, 'Aktif')
   
   -- Step 2: Insert ke tb_guru dengan id_user dari step 1
   INSERT INTO tb_guru (id_user, nip, nama_lengkap, ...)
   VALUES (LAST_INSERT_ID(), 'NIP003', 'John Doe, S.Pd', ...)
   ```

4. **Muncul pesan:**
   ```
   Data Berhasil Disimpan!
   Username: NIP003
   Password: John
   ```

5. **Guru bisa langsung login** dengan:
   - Username: `NIP003`
   - Password: `John`

---

## 📋 STORED PROCEDURES

### sp_insert_guru
```sql
CALL sp_insert_guru(
    'NIP003',              -- NIP
    'John Doe, S.Pd',      -- Nama Lengkap
    'Fisika',              -- Mata Pelajaran
    'Laki-Laki',           -- Jenis Kelamin
    'Jl. Merdeka No. 5',   -- Alamat
    '08123456789'          -- No Telepon
)
```

**Hasil:**
- ✅ User otomatis dibuat di `tb_users` dengan:
  - username = NIP (`NIP003`)
  - password = Nama depan (`John`)
  - id_role = 2 (Guru)
- ✅ Data guru disimpan di `tb_guru`

### sp_insert_siswa
```sql
CALL sp_insert_siswa(
    'NIS004',              -- NIS
    'Maria Ozawa',         -- Nama Lengkap
    'Perempuan',           -- Jenis Kelamin
    '2008-01-15',          -- Tanggal Lahir
    1,                     -- ID Kelas
    'Jl. Sudirman No. 10', -- Alamat
    'Bapak Ozawa',         -- Nama Ayah
    'Ibu Maria'            -- Nama Ibu
)
```

**Hasil:**
- ✅ User otomatis dibuat di `tb_users` dengan:
  - username = NIS (`NIS004`)
  - password = Nama depan (`Maria`)
  - id_role = 3 (Siswa)
- ✅ Data siswa disimpan di `tb_siswa`

---

## 🔄 ALUR LOGIN

```
┌─────────────────┐
│  FormLogin      │
│  - txtUsername  │
│  - txtPassword  │
│  - btnLogin     │
└────────┬────────┘
         │
         │ User Click Login
         ▼
┌─────────────────────────────────────────┐
│ Query:                                   │
│ SELECT u.*, r.nama_role FROM tb_users u │
│ INNER JOIN tb_role r ON u.id_role = r.id_role │
│ WHERE username=@user AND password=@pass  │
│ AND status='Aktif'                       │
└────────┬────────────────────────────────┘
         │
         ▼
   ┌─────────────┐
   │ Data Found? │
   └──────┬──────┘
          │
    ┌─────┴─────┐
    │           │
   Yes         No
    │           │
    ▼           ▼
┌───────────┐  ┌──────────────────┐
│ Cek Role  │  │ Login Gagal!     │
└─────┬─────┘  │ Username/Pass    │
      │        │ salah atau       │
      │        │ akun nonaktif    │
      │        └──────────────────┘
      │
   ───┴───────────────────────────
   │         │          │
   ▼         ▼          ▼
┌──────┐ ┌──────┐ ┌────────┐
│Admin │ │Guru  │ │Siswa   │
└──┬───┘ └──┬───┘ └───┬────┘
   │        │          │
   ▼        ▼          ▼
FormAdmin FormGuru FormSiswa
```

---

## 💻 IMPLEMENTASI VB.NET

### FormLogin.vb
```vb
Private Sub btnLogin_Click(...) Handles btnLogin.Click
    Try
        bukaKoneksi()
        Dim query As String = _
            "SELECT u.*, r.nama_role FROM tb_users u " & _
            "INNER JOIN tb_role r ON u.id_role = r.id_role " & _
            "WHERE u.username=@user AND u.password=@pass AND u.status='Aktif'"
        
        cmd = New MySqlCommand(query, conn)
        cmd.Parameters.AddWithValue("@user", txtUsername.Text)
        cmd.Parameters.AddWithValue("@pass", txtPassword.Text)
        dr = cmd.ExecuteReader()
        
        If dr.Read() Then
            Dim role As String = dr("nama_role").ToString()
            
            Select Case role
                Case "Admin"
                    FormAdmin.Show()
                Case "Guru"
                    FormGuru.Show()
                Case "Siswa"
                    FormSiswa.Show()
            End Select
            
            Me.Hide()
        Else
            MsgBox("Login Gagal!")
        End If
    Catch ex As Exception
        MsgBox(ex.Message)
    Finally
        conn.Close()
    End Try
End Sub
```

### FormDataGuru.vb (Button Simpan)
```vb
Private Sub Button1_Click(...) Handles Button1.Click
    Try
        koneksi()
        ' Gunakan stored procedure
        cmd = New MySqlCommand("sp_insert_guru", conn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@p_nip", TextBox1.Text)
        cmd.Parameters.AddWithValue("@p_nama_lengkap", TextBox2.Text)
        cmd.Parameters.AddWithValue("@p_mata_pelajaran", TextBox4.Text)
        cmd.Parameters.AddWithValue("@p_jenis_kelamin", jk)
        cmd.Parameters.AddWithValue("@p_alamat", TextBox3.Text)
        cmd.Parameters.AddWithValue("@p_no_telepon", TextBox5.Text)
        cmd.ExecuteNonQuery()
        
        MsgBox("Data Berhasil Disimpan!" & vbCrLf & _
               "Username: " & TextBox1.Text & vbCrLf & _
               "Password: " & TextBox2.Text.Split(" "c)(0))
    Catch ex As Exception
        MsgBox(ex.Message)
    Finally
        conn.Close()
    End Try
End Sub
```

---

## 🧪 TESTING LOGIN

### Test 1: Login sebagai Admin
```
Username: admin
Password: admin123
Expected: Redirect ke FormAdmin ✅
```

### Test 2: Login sebagai Guru
```
Username: NIP001
Password: Budi
Expected: Redirect ke FormGuru ✅
```

### Test 3: Login sebagai Siswa
```
Username: NIS001
Password: Ahmad
Expected: Redirect ke FormSiswa ✅
```

### Test 4: Input Guru Baru
1. Buka FormDataGuru
2. Isi form:
   - NIP: `NIP999`
   - Nama: `Test Guru Baru`
   - Mapel: `Biologi`
3. Klik Simpan
4. Cek pesan: **Username: NIP999, Password: Test**
5. Logout dan coba login:
   ```
   Username: NIP999
   Password: Test
   Expected: Berhasil login sebagai Guru ✅
   ```

---

## 📝 VIEWS

### v_guru_lengkap
```sql
SELECT 
    g.id_guru, g.id_user, u.username, g.nip, g.nama_lengkap,
    g.mata_pelajaran, g.jenis_kelamin, g.alamat, g.no_telepon,
    u.status, r.nama_role
FROM tb_guru g
INNER JOIN tb_users u ON g.id_user = u.id_user
INNER JOIN tb_role r ON u.id_role = r.id_role
```

### v_siswa_lengkap
```sql
SELECT 
    s.id_siswa, s.id_user, u.username, s.nis, s.nama_lengkap,
    s.jenis_kelamin, s.tanggal_lahir, k.nama_kelas,
    s.alamat, s.nama_ayah, s.nama_ibu, u.status, r.nama_role
FROM tb_siswa s
INNER JOIN tb_users u ON s.id_user = u.id_user
INNER JOIN tb_role r ON u.id_role = r.id_role
LEFT JOIN tb_kelas k ON s.id_kelas = k.id_kelas
```

---

## ⚠️ KEAMANAN

### Password Sederhana (Untuk Development)
- ✅ Password = Nama Depan (mudah diingat)
- ⚠️ Untuk production, gunakan hash password (MD5, SHA256, bcrypt)

### Status User
- **Aktif:** Bisa login
- **Nonaktif:** Tidak bisa login (meski username/password benar)

### Validasi
- Username dan password tidak boleh kosong
- User harus punya status 'Aktif'
- Role harus valid (Admin/Guru/Siswa)

---

## 🔧 CARA IMPORT DATABASE

```bash
# Via Command Line
mysql -u root < "d:\SEMESTER 3\Pemrograman visual\UTS\PROJECT PEMVIS\db_sekolah_with_login.sql"

# Via phpMyAdmin
1. Buka http://localhost/phpmyadmin
2. Klik Import
3. Pilih file: db_sekolah_with_login.sql
4. Klik Go
```

---

## ✅ CHECKLIST

- [x] Database dengan tb_role, tb_users
- [x] tb_guru dengan FK ke tb_users
- [x] tb_siswa dengan FK ke tb_users
- [x] Stored Procedure sp_insert_guru
- [x] Stored Procedure sp_insert_siswa
- [x] View v_guru_lengkap dengan JOIN
- [x] View v_siswa_lengkap dengan JOIN
- [x] FormLogin.vb dengan role-based redirect
- [x] FormDataGuru.vb auto create user
- [x] Sample data untuk testing

---

## 🎯 SUMMARY

### Saat Input Guru/Siswa Baru:
1. ✅ Otomatis create user di `tb_users`
2. ✅ Username = NIP/NIS
3. ✅ Password = Nama Depan
4. ✅ Role otomatis set (Guru=2, Siswa=3)
5. ✅ Data tersimpan di `tb_guru` atau `tb_siswa`

### Saat Login:
1. ✅ Input username & password
2. ✅ System cek di `tb_users`
3. ✅ Ambil role user
4. ✅ Redirect ke form sesuai role:
   - Admin → FormAdmin
   - Guru → FormGuru
   - Siswa → FormSiswa

**SISTEM LOGIN READY! 🎉**
