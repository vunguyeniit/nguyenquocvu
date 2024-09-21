using FluentEmail.Core;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using SoKHCNVTAPI.Entities;
using SoKHCNVTAPI.Entities.CommonCategories;
using SoKHCNVTAPI.Models;
using SoKHCNVTAPI.Repositories;
using System.Globalization;

using System.Text;
namespace SoKHCNVTAPI.Helpers
{
    public class SyncUtils
    {
        private static readonly string ClientId = "666808304cb2529fca02e755";
        private static readonly string ClientSecret = "c3ad4c415e4c48c98652e7e194450f7e";

        private static void AddHeaders(HttpClient httpClient)
        {
            httpClient.DefaultRequestHeaders.Add("Client-Id", ClientId);
            httpClient.DefaultRequestHeaders.Add("Client-Secret", ClientSecret);
        }
        public static async Task<Boolean> SyncDoanhNghiep(DoanhNghiep doanhNghiep)
        {
           
            using (var httpClient = new HttpClient())
            {
                
        // URL của API
                 var url = "https://rm-v3.systems.vn/api/data?action=create&slug=doanh-nghiep-khcn";
                // Dữ liệu bạn muốn gửi dưới dạng JSON            
                var data = new
                {

                    tendoanhnghiep = doanhNghiep.Ten,
                    tenviettat = doanhNghiep.TenVietTat,
                    Ngaythanhlap = doanhNghiep.NgayThanhLap,
                    masothue = doanhNghiep.MaSoThue,
                    dienthoai = doanhNghiep.DienThoai,
                    email = doanhNghiep.Email,
                    website = doanhNghiep.Website,
                    diachitrusochinh = doanhNghiep.DiaChi,
                    loaihinhtochuc = doanhNghiep.LoaiHinhToChuc,
                    linvucnc = doanhNghiep.LinhVucNghienCuu,
                    tinhthanh = doanhNghiep.TinhThanh,
                    loaihinh = doanhNghiep.LoaiHinh,
                };

                // Chuyển đổi dữ liệu thành JSON
                var json = JsonConvert.SerializeObject(data);
                // Đặt nội dung của request
                var content = new StringContent(json, Encoding.UTF8, "application/json");


                // Thêm Client-Id và Client-Secret vào tiêu đề
                AddHeaders(httpClient);

                // Gửi POST request
                var response = await httpClient.PostAsync(url, content);

                // Kiểm tra phản hồi từ server
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Request thanh cong!");
                    var responseContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(responseContent);
                    return true;
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Request that bai: {response.StatusCode}, Nội dung lỗi: {errorContent}");

                }
                return false;
            }
        }
       
        public static async Task<Boolean> SyncNhiemVu(NhiemVu nhiemVu)
        {
            using (var httpClient = new HttpClient())
            {
                // URL của API
                var url = "https://rm-v3.systems.vn/api/data?action=create&slug=nhiem-vu-khcn";
                // Dữ liệu bạn muốn gửi dưới dạng JSON
                var data = new
                {
                tennhiemvu = nhiemVu.Name,
                madinhdanhnhiemvuId = nhiemVu.MissionIdentifyId,
                madinhdanhnhiemvu = nhiemVu.MissionIdentify,
                capnhiemvuId = nhiemVu.MissionLevelId,
                capnhiemvu = nhiemVu.MissionLevel,
                tochucchutriId = nhiemVu.OrganizationId,
                tochucchutri = nhiemVu.Organization,
                linhvucId = nhiemVu.LinhVucNghienCuuId,
                linhvuc = nhiemVu.LinhVucNghienCuu,
                loaihinhnhiemvu = nhiemVu.LoaiHinhNhiemVu,
                thoigianthuchien = nhiemVu.TotalTimeWithMonth,
                Thoigianbatdau = nhiemVu.StartTime,
                Thoigianketthuc = nhiemVu.EndTime,
                tongkinhphi = nhiemVu.TotalExpenses,
                trangthai = nhiemVu.Status,
            };

                // Chuyển đổi dữ liệu thành JSON
                var json = JsonConvert.SerializeObject(data);
                // Đặt nội dung của request
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // Thêm Client-Id và Client-Secret vào tiêu đề
                AddHeaders(httpClient);

                // Gửi POST request
                var response = await httpClient.PostAsync(url, content);

                // Kiểm tra phản hồi từ server
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Request thanh cong!");
                    var responseContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(responseContent);
                    return true;
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Request that bai: {response.StatusCode}, Nội dung lỗi: {errorContent}");

                }
                return false;
            }
        }

        public static async Task<Boolean> SyncToChuc(ToChuc toChuc)
        {
            using (var httpClient = new HttpClient())
            {
                // URL của API
                var url = "https://rm-v3.systems.vn/api/data?action=create&slug=to-chuc-khcn";
                // Dữ liệu bạn muốn gửi dưới dạng JSON
                var data = new
                {
                        tentochuc = toChuc.TenToChuc,
                        madinhdanhtochuc = toChuc.OrganizationIdentifierId,
                        diachi = toChuc.DiaChi,
                        tinhthanh = toChuc.TinhThanh,
                        dienthoai = toChuc.DienThoai,
                        website = toChuc.Website,
                        email= toChuc.Email,
                        nguoidungdau = toChuc.NguoiDungDau,
                        loaihinhtochuc = toChuc.LoaiHinhToChuc,
                        hinhthucsohuu = toChuc.HinhThucSoHuu,
                        linhvucnc = toChuc.LinhVucNC,
            };
                // Chuyển đổi dữ liệu thành JSON
                var json = JsonConvert.SerializeObject(data);
                // Đặt nội dung của request
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // Thêm Client-Id và Client-Secret vào tiêu đề
                AddHeaders(httpClient);

                // Gửi POST request
                var response = await httpClient.PostAsync(url, content);
                // Kiểm tra phản hồi từ server
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Request thanh cong!");
                    var responseContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(responseContent);
                    return true;
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Request that bai: {response.StatusCode}, Nội dung lỗi: {errorContent}");
                }
                return false;
            }
        }

        public static async Task<Boolean> SyncCanBo(CanBo canBo)
        {
            using (var httpClient = new HttpClient())
            {
                // URL của API
                var url = " https://rm-v3.systems.vn/api/data?action=create&slug=can-bo-khcn";
                // Dữ liệu bạn muốn gửi dưới dạng JSON
                var data = new
                {
                        hovaten = canBo.HoVaTen,
                        madinhdanhcanbo = canBo.Ma,
                        Ngaysinh = canBo.NgaySinh,
                        gioitinh = canBo.GioiTinh,
                        quoctich = canBo.QuocTich,
                        cccd = canBo.CCCD,
                        noiohiennay = canBo.NoiOHienNay,
                        tinhthanh = canBo.TinhThanh,
                        dienthoai = canBo.DienThoai,
                        email = canBo.Email,
                        chucdanhnghenghiep = canBo.ChucDanhNgheNghiep,
                        chucdanh = canBo.ChucDanh,
                        namphongchucdanh = canBo.NamPhongChucDanh,
                        hocvi = canBo.HocVi,
                        namdathocvi = canBo.NamDatHocVi,
                        coquancongtac = canBo.CoQuanCongTac,
                        linhvucnc = canBo.LinhVucNC,
            };
                // Chuyển đổi dữ liệu thành JSON
                var json = JsonConvert.SerializeObject(data);
                // Đặt nội dung của request
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // Thêm Client-Id và Client-Secret vào tiêu đề
                AddHeaders(httpClient);

                // Gửi POST request
                var response = await httpClient.PostAsync(url, content);
                // Kiểm tra phản hồi từ server
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Request thanh cong!");
                    var responseContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(responseContent);
                    return true;
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Request that bai: {response.StatusCode}, Nội dung lỗi: {errorContent}");
                }
                return false;
            }
        }

        public static async Task<Boolean> SyncChuyenGia(ChuyenGia chuyenGia)
        {
            using (var httpClient = new HttpClient())
            {
                // URL của API
                var url = "https://rm-v3.systems.vn/api/data?action=create&slug=chuyen-gia-khcn";
                // Dữ liệu bạn muốn gửi dưới dạng JSON
                var data = new
                {
                    
                        hovaten = chuyenGia.HovaTen,
                        Ngaysinh = chuyenGia.NgaySinh,
                        quoctich = chuyenGia.QuocTich,
                        hocvi = chuyenGia.HocVi,
                        bangcap = chuyenGia.BangCap,
                        linhvucchuyenmon = chuyenGia.LVChuyenMon,
                        thongtinlienhe = chuyenGia.ThongTinLienHe,
                        sodienthoai = chuyenGia.SDT,
                        email = chuyenGia.Email,
                        diachi = chuyenGia.DiaChi,
            };
                // Chuyển đổi dữ liệu thành JSON
                var json = JsonConvert.SerializeObject(data);
                // Đặt nội dung của request
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // Thêm Client-Id và Client-Secret vào tiêu đề
                AddHeaders(httpClient);

                // Gửi POST request
                var response = await httpClient.PostAsync(url, content);
                // Kiểm tra phản hồi từ server
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Request thanh cong!");
                    var responseContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(responseContent);
                    return true;
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Request that bai: {response.StatusCode}, Nội dung lỗi: {errorContent}");
                }
                return false;
            }
        }

        public static async Task<Boolean> SyncSoHuuTriTue(SoHuuTriTue soHuuTriTue)
        {
            using (var httpClient = new HttpClient())
            {
                // URL của API
                var url = "https://rm-v3.systems.vn/api/data?action=create&slug=so-huu-tri-tue";
                // Dữ liệu bạn muốn gửi dưới dạng JSON
                var data = new
                {
                    ten = soHuuTriTue.TenSangChe,
                    so_bang = soHuuTriTue.SoBang,
                    loai_shcn = soHuuTriTue.LoaiSoHuu,
                    phan_loai_sang_che_quoc_te = soHuuTriTue.PhanLoai,
                    chu_bang = soHuuTriTue.ChuBang,
                    thong_tin_so_huu = soHuuTriTue.ThongTinSoHuu,
                    sang_che_toan_van = soHuuTriTue.SangCheToanVan,
                    tc_dai_dien_shcn = soHuuTriTue.ToChucDaiDien,
                    nguoi_dai_dien_shcn = soHuuTriTue.NguoiDaiDien,
                    tc_du_dieu_kien = soHuuTriTue.ToChucDuDieuKien,
                    ca_nhan_du_dieu_kien = soHuuTriTue.CaNhanDuDieuKien,
                };
                // Chuyển đổi dữ liệu thành JSON
                var json = JsonConvert.SerializeObject(data);
                // Đặt nội dung của request
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // Thêm Client-Id và Client-Secret vào tiêu đề
                AddHeaders(httpClient);
                // Gửi POST request
                var response = await httpClient.PostAsync(url, content);
                // Kiểm tra phản hồi từ server
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Request thanh cong!");
                    var responseContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(responseContent);
                    return true;
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Request that bai: {response.StatusCode}, Nội dung lỗi: {errorContent}");
                }
                return false;
            }
        }

        public static async Task<Boolean> SyncCongBo(CongBo congBo)
        {
            using (var httpClient = new HttpClient())
            {
                // URL của API
                var url = "https://rm-v3.systems.vn/api/data?action=create&slug=cong-bo-khoa-hoc-va-chi-so-trich-dan-khoa-hoc";
                // Dữ liệu bạn muốn gửi dưới dạng JSON
                var data = new
                {
                    chi_so_de_muc = congBo.ChiSoDeMuc,
                    linh_vuc_nghien_cuu = congBo.LinhVucNghienCuu,
                    dang_tai_lieu = congBo.DangTaiLieu,
                    tac_gia = congBo.TacGia,
                    nhan_de = congBo.NhanDe,
                    nguon_trich = congBo.NguonTrich,
                    nam = congBo.NamXuatBan,
                    so = congBo.So,
                    trang = congBo.Trang,
                    issn = congBo.ISSN,
                    tu_khoa = congBo.TuKhoa,
                    tom_tat = congBo.TomTat,
                    ky_hieu_kho = congBo.KyHieuKho,
                    xem_toan_van = congBo.XemToanVan,
                };
                // Chuyển đổi dữ liệu thành JSON
                var json = JsonConvert.SerializeObject(data);
                // Đặt nội dung của request
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // Thêm Client-Id và Client-Secret vào tiêu đề
                AddHeaders(httpClient);
                // Gửi POST request
                var response = await httpClient.PostAsync(url, content);
                // Kiểm tra phản hồi từ server
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Request thanh cong!");
                    var responseContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(responseContent);
                    return true;
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Request that bai: {response.StatusCode}, Nội dung lỗi: {errorContent}");
                }
                return false;
            }
        }

        public static async Task<Boolean> SyncThongTinKHCN(ThongTin thongTin)
        {
            using (var httpClient = new HttpClient())
            {
                // URL của API
                var url = " https://rm-v3.systems.vn/api/data?action=create&slug=thong-tin-khcn-khu-vuc";
                // Dữ liệu bạn muốn gửi dưới dạng JSON
                var data = new
                {
                    ma_so_quoc_gia = thongTin.MaSoQuocGia,
                    ten_quoc_gia = thongTin.TenQuocGia,
                    ThoiGianSoLieuThongKe = thongTin.ThoiGian,
                    tk_nhan_luc = thongTin.ThongKeNhanLuc,
                    tk_kinh_phi_nckh = thongTin.ThongKeKinhPhi,
                    tk_ket_qua_hoat_dong_nckh = thongTin.ThongKeKetQua,
                    tk_hoat_dong_dmst = thongTin.ThongKeHoatDong,
          
                };
                // Chuyển đổi dữ liệu thành JSON
                var json = JsonConvert.SerializeObject(data);
                // Đặt nội dung của request
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // Thêm Client-Id và Client-Secret vào tiêu đề
                AddHeaders(httpClient);
                // Gửi POST request
                var response = await httpClient.PostAsync(url, content);
                // Kiểm tra phản hồi từ server
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Request thanh cong!");
                    var responseContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(responseContent);
                    return true;
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Request that bai: {response.StatusCode}, Nội dung lỗi: {errorContent}");
                }
                return false;
            }
        }

        //SyncImport
        public static async Task<Boolean> SyncImportDoanhNghiep(DoanhNghiep doanhNghiep)
        {
            using (var httpClient = new HttpClient())
            {
                // URL của API
                var url = "https://rm-v3.systems.vn/api/data?action=import&slug=doanh-nghiep-khcn";
                // Dữ liệu bạn muốn gửi dưới dạng JSON            
                 var data = new[]
                   {
                    new
                    {
                        tendoanhnghiep = doanhNghiep.Ten,
                        tenviettat = doanhNghiep.TenVietTat,
                        Ngaythanhlap = doanhNghiep.NgayThanhLap,
                        masothue = doanhNghiep.MaSoThue,
                        dienthoai = doanhNghiep.DienThoai,
                        email = doanhNghiep.Email,
                        website = doanhNghiep.Website,
                        diachitrusochinh = doanhNghiep.DiaChi,
                        loaihinhtochuc = doanhNghiep.LoaiHinhToChuc,
                        linvucnc = doanhNghiep.LinhVucNghienCuu,
                        tinhthanh = doanhNghiep.TinhThanh,
                        loaihinh = doanhNghiep.LoaiHinh,
                    }
                };

                // Chuyển đổi dữ liệu thành JSON
                var json = JsonConvert.SerializeObject(data);
                // Đặt nội dung của request
                var content = new StringContent(json, Encoding.UTF8, "application/json");


                // Thêm Client-Id và Client-Secret vào tiêu đề
                AddHeaders(httpClient);

                // Gửi POST request
                var response = await httpClient.PostAsync(url, content);

                // Kiểm tra phản hồi từ server
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Request thanh cong!");
                    var responseContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(responseContent);
                    return true;
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Request that bai: {response.StatusCode}, Nội dung lỗi: {errorContent}");

                }
                return false;
            }
        }

        public static async Task<Boolean> SyncImportNhiemVu(NhiemVu nhiemVu)
        {
            using (var httpClient = new HttpClient())
            {
                // URL của API
                var url = "https://rm-v3.systems.vn/api/data?action=import&slug=nhiem-vu-khcn";
                // Dữ liệu bạn muốn gửi dưới dạng JSON            
                var data = new[]
                   {
                    new
                    {
                        tennhiemvu = nhiemVu.Name,
                        madinhdanhnhiemvuId = nhiemVu.MissionIdentifyId,
                        madinhdanhnhiemvu = nhiemVu.MissionIdentify,
                        capnhiemvuId = nhiemVu.MissionLevelId,
                        capnhiemvu = nhiemVu.MissionLevel,
                        tochucchutriId = nhiemVu.OrganizationId,
                        tochucchutri = nhiemVu.Organization,
                        linhvucId = nhiemVu.LinhVucNghienCuuId,
                        linhvuc = nhiemVu.LinhVucNghienCuu,
                        loaihinhnhiemvu = nhiemVu.LoaiHinhNhiemVu,
                        thoigianthuchien = nhiemVu.TotalTimeWithMonth,
                        Thoigianbatdau = nhiemVu.StartTime,
                        Thoigianketthuc = nhiemVu.EndTime,
                        tongkinhphi = nhiemVu.TotalExpenses,
                        trangthai = nhiemVu.Status,
                    }
                };


                // Chuyển đổi dữ liệu thành JSON
                var json = JsonConvert.SerializeObject(data);
                // Đặt nội dung của request
                var content = new StringContent(json, Encoding.UTF8, "application/json");


                // Thêm Client-Id và Client-Secret vào tiêu đề
                AddHeaders(httpClient);

                // Gửi POST request
                var response = await httpClient.PostAsync(url, content);

                // Kiểm tra phản hồi từ server
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Request thanh cong!");
                    var responseContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(responseContent);
                    return true;
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Request that bai: {response.StatusCode}, Nội dung lỗi: {errorContent}");

                }
                return false;
            }
        }

        public static async Task<Boolean> SyncImportToChuc(ToChuc toChuc)
        {
            using (var httpClient = new HttpClient())
            {
                // URL của API
                var url = " https://rm-v3.systems.vn/api/data?action=import&slug=to-chuc-khcn";
                // Dữ liệu bạn muốn gửi dưới dạng JSON            
                var data = new[]
                   {
                    new
                    {
                       tentochuc = toChuc.TenToChuc,
                        madinhdanhtochuc = toChuc.OrganizationIdentifierId,
                        diachi = toChuc.DiaChi,
                        tinhthanh = toChuc.TinhThanh,
                        dienthoai = toChuc.DienThoai,
                        website = toChuc.Website,
                        email= toChuc.Email,
                        nguoidungdau = toChuc.NguoiDungDau,
                        loaihinhtochuc = toChuc.LoaiHinhToChuc,
                        hinhthucsohuu = toChuc.HinhThucSoHuu,
                        linhvucnc = toChuc.LinhVucNC,
                    }
                };


                // Chuyển đổi dữ liệu thành JSON
                var json = JsonConvert.SerializeObject(data);
                // Đặt nội dung của request
                var content = new StringContent(json, Encoding.UTF8, "application/json");


                // Thêm Client-Id và Client-Secret vào tiêu đề
                AddHeaders(httpClient);

                // Gửi POST request
                var response = await httpClient.PostAsync(url, content);

                // Kiểm tra phản hồi từ server
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Request thanh cong!");
                    var responseContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(responseContent);
                    return true;
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Request that bai: {response.StatusCode}, Nội dung lỗi: {errorContent}");

                }
                return false;
            }
        }

        public static async Task<Boolean> SyncImportCanBo(CanBo canBo)
        {
            using (var httpClient = new HttpClient())
            {
                // URL của API
                var url = " https://rm-v3.systems.vn/api/data?action=import&slug=can-bo-khcn";
                // Dữ liệu bạn muốn gửi dưới dạng JSON            
                var data = new[]
                   {
                    new
                    {
                        hovaten = canBo.HoVaTen,
                        madinhdanhcanbo = canBo.Ma,
                        Ngaysinh = canBo.NgaySinh,
                        gioitinh = canBo.GioiTinh,
                        quoctich = canBo.QuocTich,
                        cccd = canBo.CCCD,
                        noiohiennay = canBo.NoiOHienNay,
                        tinhthanh = canBo.TinhThanh,
                        dienthoai = canBo.DienThoai,
                        email = canBo.Email,
                        chucdanhnghenghiep = canBo.ChucDanhNgheNghiep,
                        chucdanh = canBo.ChucDanh,
                        namphongchucdanh = canBo.NamPhongChucDanh,
                        hocvi = canBo.HocVi,
                        namdathocvi = canBo.NamDatHocVi,
                        coquancongtac = canBo.CoQuanCongTac,
                        linhvucnc = canBo.LinhVucNC,
                    }
                };


                // Chuyển đổi dữ liệu thành JSON
                var json = JsonConvert.SerializeObject(data);
                // Đặt nội dung của request
                var content = new StringContent(json, Encoding.UTF8, "application/json");


                // Thêm Client-Id và Client-Secret vào tiêu đề
                AddHeaders(httpClient);

                // Gửi POST request
                var response = await httpClient.PostAsync(url, content);

                // Kiểm tra phản hồi từ server
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Request thanh cong!");
                    var responseContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(responseContent);
                    return true;
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Request that bai: {response.StatusCode}, Nội dung lỗi: {errorContent}");

                }
                return false;
            }
        }

        public static async Task<Boolean> SyncImportChuyenGia(ChuyenGia chuyenGia)
        {
            using (var httpClient = new HttpClient())
            {
                // URL của API
                var url = "https://rm-v3.systems.vn/api/data?action=import&slug=chuyen-gia-khcn";
                // Dữ liệu bạn muốn gửi dưới dạng JSON            
                var data = new[]
                   {
                    new
                    {
                        hovaten = chuyenGia.HovaTen,
                        Ngaysinh = chuyenGia.NgaySinh,
                        quoctich = chuyenGia.QuocTich,
                        hocvi = chuyenGia.HocVi,
                        bangcap = chuyenGia.BangCap,
                        linhvucchuyenmon = chuyenGia.LVChuyenMon,
                        thongtinlienhe = chuyenGia.ThongTinLienHe,
                        sodienthoai = chuyenGia.SDT,
                        email = chuyenGia.Email,
                        diachi = chuyenGia.DiaChi,
                    }
                };

                // Chuyển đổi dữ liệu thành JSON
                var json = JsonConvert.SerializeObject(data);
                // Đặt nội dung của request
                var content = new StringContent(json, Encoding.UTF8, "application/json");


                // Thêm Client-Id và Client-Secret vào tiêu đề
                AddHeaders(httpClient);

                // Gửi POST request
                var response = await httpClient.PostAsync(url, content);

                // Kiểm tra phản hồi từ server
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Request thanh cong!");
                    var responseContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(responseContent);
                    return true;
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Request that bai: {response.StatusCode}, Nội dung lỗi: {errorContent}");

                }
                return false;
            }
        }

        public static async Task<Boolean> SyncImportSoHuuTriTue(SoHuuTriTue soHuuTriTue)
        {
            using (var httpClient = new HttpClient())
            {
                // URL của API
                var url = "https://rm-v3.systems.vn/api/data?action=import&slug=so-huu-tri-tue";
                // Dữ liệu bạn muốn gửi dưới dạng JSON            
                var data = new[]
                   {
                    new
                    {
                            ten = soHuuTriTue.TenSangChe,
                            so_bang = soHuuTriTue.SoBang,
                            loai_shcn = soHuuTriTue.LoaiSoHuu,
                            phan_loai_sang_che_quoc_te = soHuuTriTue.PhanLoai,
                            chu_bang = soHuuTriTue.ChuBang,
                            thong_tin_so_huu = soHuuTriTue.ThongTinSoHuu,
                            sang_che_toan_van = soHuuTriTue.SangCheToanVan,
                            tc_dai_dien_shcn = soHuuTriTue.ToChucDaiDien,
                            nguoi_dai_dien_shcn = soHuuTriTue.NguoiDaiDien,
                            tc_du_dieu_kien = soHuuTriTue.ToChucDuDieuKien,
                            ca_nhan_du_dieu_kien = soHuuTriTue.CaNhanDuDieuKien,
                    }
                };

                // Chuyển đổi dữ liệu thành JSON
                var json = JsonConvert.SerializeObject(data);
                // Đặt nội dung của request
                var content = new StringContent(json, Encoding.UTF8, "application/json");


                // Thêm Client-Id và Client-Secret vào tiêu đề
                AddHeaders(httpClient);

                // Gửi POST request
                var response = await httpClient.PostAsync(url, content);

                // Kiểm tra phản hồi từ server
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Request thanh cong!");
                    var responseContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(responseContent);
                    return true;
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Request that bai: {response.StatusCode}, Nội dung lỗi: {errorContent}");

                }
                return false;
            }
        }

        public static async Task<Boolean> SyncImportCongBo(CongBo congBo)
        {
            using (var httpClient = new HttpClient())
            {
                // URL của API
                var url = "https://rm-v3.systems.vn/api/data?action=import&slug=cong-bo-khoa-hoc-va-chi-so-trich-dan-khoa-hoc";
                // Dữ liệu bạn muốn gửi dưới dạng JSON            
                var data = new[]
                   {
                    new
                    {
                    chi_so_de_muc = congBo.ChiSoDeMuc,
                    linh_vuc_nghien_cuu = congBo.LinhVucNghienCuu,
                    dang_tai_lieu = congBo.DangTaiLieu,
                    tac_gia = congBo.TacGia,
                    nhan_de = congBo.NhanDe,
                    nguon_trich = congBo.NguonTrich,
                    nam = congBo.NamXuatBan,
                    so = congBo.So,
                    trang = congBo.Trang,
                    issn = congBo.ISSN,
                    tu_khoa = congBo.TuKhoa,
                    tom_tat = congBo.TomTat,
                    ky_hieu_kho = congBo.KyHieuKho,
                    xem_toan_van = congBo.XemToanVan,
                    }
                };

                // Chuyển đổi dữ liệu thành JSON
                var json = JsonConvert.SerializeObject(data);
                // Đặt nội dung của request
                var content = new StringContent(json, Encoding.UTF8, "application/json");


                // Thêm Client-Id và Client-Secret vào tiêu đề
                AddHeaders(httpClient);

                // Gửi POST request
                var response = await httpClient.PostAsync(url, content);

                // Kiểm tra phản hồi từ server
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Request thanh cong!");
                    var responseContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(responseContent);
                    return true;
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Request that bai: {response.StatusCode}, Nội dung lỗi: {errorContent}");

                }
                return false;
            }
        }

        public static async Task<Boolean> SyncImportThongTinKHCN(ThongTin thongTin)
        {
            using (var httpClient = new HttpClient())
            {
                // URL của API
                var url = "https://rm-v3.systems.vn/api/data?action=import&slug=thong-tin-khcn-khu-vuc";
                // Dữ liệu bạn muốn gửi dưới dạng JSON          
                var data = new[]
                  {
                    new
                    {
                    ma_so_quoc_gia = thongTin.MaSoQuocGia,
                    ten_quoc_gia = thongTin.TenQuocGia,
                    ThoiGianSoLieuThongKe = thongTin.ThoiGian,
                    tk_nhan_luc = thongTin.ThongKeNhanLuc,
                    tk_kinh_phi_nckh = thongTin.ThongKeKinhPhi,
                    tk_ket_qua_hoat_dong_nckh = thongTin.ThongKeKetQua,
                    tk_hoat_dong_dmst = thongTin.ThongKeHoatDong,
                    }
                };
                // Chuyển đổi dữ liệu thành JSON
                var json = JsonConvert.SerializeObject(data);
                // Đặt nội dung của request
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // Thêm Client-Id và Client-Secret vào tiêu đề
                AddHeaders(httpClient);
                // Gửi POST request
                var response = await httpClient.PostAsync(url, content);
                // Kiểm tra phản hồi từ server
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Request thanh cong!");
                    var responseContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(responseContent);
                    return true;
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Request that bai: {response.StatusCode}, Nội dung lỗi: {errorContent}");
                }
                return false;
            }
        }
    }
}
