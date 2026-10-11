# eduhub
*   **Họ và tên:** Lê Ngọc Yến Vy
*   **Mã số sinh viên (MSSV):** 0024419637

*   ## Bài tập Chương 1 - IN4211
*   1. **Lệnh tạo project console mới và lệnh chạy nó?**
   - Lệnh tạo project: `dotnet new console -n <Tên_Project>`
   - Lệnh chạy project: `dotnet run`
2. **Sơ đồ lớp của EduHub:**
3. **Class Member và vị trí kiểm tra Email:**
   - Các thuộc tính đều dùng `private set` và `constructor private`.
   - Kiểm tra Email nằm ở hàm Factory `Create()` trước khi khởi tạo đối tượng.
4. **Thứ tự các bước từ clone đến Pull Request:**
   - `git clone`
   - `git switch -c` (tạo và chuyển nhánh)
   - `git add .`
   - `git commit`
   - `git push`
   - Mở Pull Request trên GitHub
5. **Commit message khi thêm HasCapacity():**
   - `feat(domain): add HasCapacity method to Event`
