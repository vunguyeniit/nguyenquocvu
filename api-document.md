# API Document

## Thông tin dự án
- [Net 8](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- DBMS: PostgreSQL

## Cấu trúc thư mục

1. Controller: Nơi chứa, định nghĩa các api endpoint
2. Entities: Các thực thể Entity mapping với các table trong database
3. Models: Các Dto (Data tranfer object), model
4. Requests: Nơi chứa model, class được gửi từ client như form, json,...
5. Responses: Nơi chức các model, class trả về cho client
6. Services: Chứa logic ứng dụng (bỏ)
7. Repositories: hiện tại chứa logic ứng dụng
8. Configurations: Chứa Database Context, cấu hình Mapper
9. Extensions: ...
10. Migrations: (Không sử dụng)
11. Helpers: chứa các hàm, class helper

## Một api gồm có

1. Entity, Dto hoặc ModelRequest, ModelResponse
2. Controller
3. Logic nghiệp vụ