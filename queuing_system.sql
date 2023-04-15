-- phpMyAdmin SQL Dump
-- version 5.2.0
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Apr 08, 2023 at 06:56 AM
-- Server version: 10.4.25-MariaDB
-- PHP Version: 8.0.23

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `queuing_system`
--

-- --------------------------------------------------------

--
-- Table structure for table `account`
--

CREATE TABLE `account` (
  `id` bigint(20) UNSIGNED NOT NULL,
  `username` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `fullname` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `phone` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `email` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `password` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `confirm_password` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `role` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `status` tinyint(1) NOT NULL DEFAULT 0,
  `created_at` timestamp NULL DEFAULT NULL,
  `updated_at` timestamp NULL DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

--
-- Dumping data for table `account`
--

INSERT INTO `account` (`id`, `username`, `fullname`, `phone`, `email`, `password`, `confirm_password`, `role`, `status`, `created_at`, `updated_at`) VALUES
(1, 'nguyenlinh2002', 'Nguyen Van A', '0788990941', 'NguyenA154@gmail.com', 'nguyenlinh2002', 'nguyenlinh2002', 'Quản lý', 0, '2023-03-16 03:53:46', '2023-03-25 04:41:53'),
(2, 'tuyennguyen123', 'Nguyen Van B', '0788990951', 'NguyenA114@gmail.com', 'tuyennguyen123', 'tuyennguyen123', 'Admin', 0, '2023-03-19 06:39:36', '2023-03-19 06:39:36'),
(3, 'tuyennguyen123', 'Nguyen Van A', '0788990948', 'NguyenA154@gmail.com', 'tuyennguyen123', 'tuyennguyen123', 'Quản lý', 1, '2023-03-19 06:41:42', '2023-03-19 06:41:42'),
(4, 'tuyennguyen123', 'Nguyen Van B', '0374720761', 'NguyenA154@gmail.com', 'tuyennguyen123', 'tuyennguyen123', 'Quản lý', 0, '2023-03-25 02:35:34', '2023-03-25 02:35:34'),
(5, 'Linhkyo001', 'Nguyen Van C', '0788990941', 'NguyenA154@gmail.com', 'Linhkyo001', 'Linhkyo001', 'Admin', 1, '2023-03-25 04:42:27', '2023-03-25 04:42:27');

-- --------------------------------------------------------

--
-- Table structure for table `cumtomer_service`
--

CREATE TABLE `cumtomer_service` (
  `id` bigint(20) UNSIGNED NOT NULL,
  `user_id` int(11) NOT NULL,
  `ser_id` int(11) NOT NULL,
  `created_at` timestamp NULL DEFAULT NULL,
  `updated_at` timestamp NULL DEFAULT NULL,
  `user_print` tinyint(1) NOT NULL DEFAULT 0
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

--
-- Dumping data for table `cumtomer_service`
--

INSERT INTO `cumtomer_service` (`id`, `user_id`, `ser_id`, `created_at`, `updated_at`, `user_print`) VALUES
(1, 1, 1, '2023-03-16 03:42:57', '2023-03-16 03:42:57', 1),
(2, 1, 2, '2023-03-16 03:42:57', '2023-03-16 03:42:57', 1),
(3, 2, 1, '2023-03-16 03:43:40', '2023-03-16 03:43:40', 1),
(4, 2, 2, '2023-03-16 03:43:40', '2023-03-16 03:43:40', 1),
(7, 3, 4, '2023-03-08 13:34:15', '2023-03-08 13:34:15', 1),
(8, 4, 2, '2023-03-08 13:34:48', '2023-03-08 13:34:48', 1),
(9, 4, 3, '2023-03-08 13:36:08', '2023-03-08 13:36:08', 1),
(10, 5, 4, '2023-03-08 13:36:51', '2023-03-08 13:36:51', 1),
(11, 5, 1, '2023-03-08 13:37:21', '2023-03-08 13:37:21', 1),
(12, 5, 2, '2023-03-08 13:37:42', '2023-03-08 13:37:42', 1),
(13, 6, 3, '2023-03-08 13:38:10', '2023-03-08 13:38:10', 1);

-- --------------------------------------------------------

--
-- Table structure for table `customer`
--

CREATE TABLE `customer` (
  `id` bigint(20) UNSIGNED NOT NULL,
  `fullname` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `phone` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `email` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `created_at` timestamp NULL DEFAULT NULL,
  `updated_at` timestamp NULL DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

--
-- Dumping data for table `customer`
--

INSERT INTO `customer` (`id`, `fullname`, `phone`, `email`, `created_at`, `updated_at`) VALUES
(1, 'Nguyễn Thị Dung', '0128321421', 'nguyendung@gmail.com', '2023-03-16 03:40:20', '2023-03-16 03:40:20'),
(2, 'Nguyễn Thị My', '0128321422', 'nguyenmy@gmail.com', '2023-03-16 03:40:20', '2023-03-16 03:40:20'),
(3, 'Nguyễn Văn Minh', '0293842428', 'nguyenminh@gmail.com', '2023-03-08 13:30:09', '2023-03-08 13:30:09'),
(4, 'Nguyễn Văn Chí', '0293842428', 'nguyenchi@gmail.com', '2023-03-08 13:30:09', '2023-03-08 13:30:09'),
(5, 'Nguyễn Minh Khôi', '0967328393', 'nguyenkhoi@gmail.com', '2023-03-12 13:31:51', '2023-03-12 13:31:51'),
(6, 'Nguyễn Minh Duy', '0967328393', 'nguyenduy@gmail.com', '2023-03-12 13:31:51', '2023-03-12 13:31:51');

-- --------------------------------------------------------

--
-- Table structure for table `device`
--

CREATE TABLE `device` (
  `id` bigint(20) UNSIGNED NOT NULL,
  `devicecode` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `devicename` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `devicetype` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `username` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `addressip` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `password` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `activestatus` tinyint(1) NOT NULL DEFAULT 0,
  `connectionstatus` tinyint(1) NOT NULL DEFAULT 0,
  `created_at` timestamp NULL DEFAULT NULL,
  `updated_at` timestamp NULL DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

--
-- Dumping data for table `device`
--

INSERT INTO `device` (`id`, `devicecode`, `devicename`, `devicetype`, `username`, `addressip`, `password`, `activestatus`, `connectionstatus`, `created_at`, `updated_at`) VALUES
(1, 'KIO_01', 'Kiosk', 'Kiosk', 'Linhkyo001', '128.172.308', 'CMS', 1, 0, '2023-03-17 07:22:01', '2023-03-17 07:22:01'),
(2, 'KIO_01', 'Kiosk', 'Kiosk', 'Linhkyo001', '128.172.308', 'CMS', 0, 1, '2023-03-17 07:23:19', '2023-03-17 07:23:19'),
(3, 'KIO_01', 'Kiosk', 'Kiosk', 'Linhkyo001', '128.172.308', 'CMS', 0, 1, '2023-03-17 10:11:44', '2023-03-17 10:11:44'),
(4, 'KIO_01', 'Kiosk', 'Kiosk', 'Linhkyo001', '128.172.308', '123456', 0, 0, '2023-03-25 03:49:21', '2023-03-25 03:49:21'),
(5, 'KIO_01', 'Kiosk1', 'Kiosk', 'Linhkyo001', '128.172.308', '12345', 0, 0, '2023-03-25 04:15:29', '2023-03-25 04:28:34'),
(6, 'KIO_01', 'Kiosk1', 'Kiosk', 'Linhkyo001', '128.172.308', '123456789', 0, 0, '2023-03-25 04:28:05', '2023-03-25 04:28:05'),
(7, 'KIO_01', 'Kiosk', 'Display counter', 'Linhkyo001', '128.172.308', '123', 0, 0, '2023-03-28 09:29:51', '2023-03-28 09:29:51');

-- --------------------------------------------------------

--
-- Table structure for table `diary`
--

CREATE TABLE `diary` (
  `id` bigint(20) UNSIGNED NOT NULL,
  `username` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `usetime` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `ip` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `perform` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `created_at` timestamp NULL DEFAULT NULL,
  `updated_at` timestamp NULL DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

--
-- Dumping data for table `diary`
--

INSERT INTO `diary` (`id`, `username`, `usetime`, `ip`, `perform`, `created_at`, `updated_at`) VALUES
(2, 'tuyetnguyen@12', '21-03-2023 16:14:13 ', '192.168.3.1', 'Cập nhật thông tin dịch vụ DV_01', '2023-03-01 09:14:34', '2023-03-21 09:14:34'),
(3, 'tuyetnguyen@12', '21-03-2023 16:17:04 ', '192.168.3.1', 'Cập nhật thông tin dịch vụ DV_01', '2023-03-04 09:17:04', '2023-03-21 09:17:04'),
(4, 'tuyetnguyen@12', '21-03-2023 16:17:38 ', '192.168.3.1', 'Cập nhật thông tin dịch vụ DV_01', '2023-03-05 09:17:38', '2023-03-21 09:17:38'),
(5, 'tuyetnguyen@12', '21-03-2023 16:17:59 ', '192.168.3.1', 'Cập nhật thông tin dịch vụ DV_01', '2023-03-06 09:17:59', '2023-03-21 09:17:59'),
(6, 'tuyetnguyen@12', '21-03-2023 16:18:07 ', '192.168.3.1', 'Cập nhật thông tin dịch vụ DV_01', '2023-03-08 09:18:07', '2023-03-21 09:18:07'),
(7, 'tuyetnguyen@11', '21-03-2023 16:18:21 ', '192.168.5.1', 'Cập nhật thông tin dịch vụ DV_01', '2023-03-21 09:18:21', '2023-03-21 09:18:21'),
(8, 'tuyetnguyen@12', '21-03-2023 16:18:42 ', '192.168.4.1', 'Cập nhật thông tin dịch vụ DV_01', '2023-03-21 09:18:42', '2023-03-21 09:18:42'),
(9, 'tuyetnguyen@12', '21-03-2023 16:19:04 ', '192.168.4.1', 'Cập nhật thông tin dịch vụ DV_01', '2023-03-21 09:19:04', '2023-03-21 09:19:04'),
(10, 'tuyetnguyen@15', '21-03-2023 16:19:10 ', '192.168.3.1', 'Cập nhật thông tin dịch vụ DV_01', '2023-03-21 09:19:10', '2023-03-21 09:19:10');

-- --------------------------------------------------------

--
-- Table structure for table `failed_jobs`
--

CREATE TABLE `failed_jobs` (
  `id` bigint(20) UNSIGNED NOT NULL,
  `uuid` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `connection` text COLLATE utf8_unicode_ci NOT NULL,
  `queue` text COLLATE utf8_unicode_ci NOT NULL,
  `payload` longtext COLLATE utf8_unicode_ci NOT NULL,
  `exception` longtext COLLATE utf8_unicode_ci NOT NULL,
  `failed_at` timestamp NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- --------------------------------------------------------

--
-- Table structure for table `migrations`
--

CREATE TABLE `migrations` (
  `id` int(10) UNSIGNED NOT NULL,
  `migration` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `batch` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

--
-- Dumping data for table `migrations`
--

INSERT INTO `migrations` (`id`, `migration`, `batch`) VALUES
(1, '2014_10_12_000000_create_users_table', 1),
(2, '2014_10_12_100000_create_password_resets_table', 1),
(3, '2019_08_19_000000_create_failed_jobs_table', 1),
(4, '2019_12_14_000001_create_personal_access_tokens_table', 1),
(5, '2023_02_22_055429_create_user_table', 1),
(6, '2023_02_22_155642_add_colum_table', 1),
(7, '2023_03_03_132648_create_device_table', 1),
(8, '2023_03_05_203015_create_tag_device', 1),
(9, '2023_03_05_203716_create_tagid_device', 1),
(10, '2023_03_09_095459_create_service_tabel', 1),
(11, '2023_03_12_200716_create_service_tabel', 1),
(12, '2023_03_13_121526_create_customer_tabel', 1),
(13, '2023_03_13_122124_create_number_print__table', 1),
(14, '2023_03_13_162714_create_customer_service_table', 1),
(15, '2023_03_15_105002_create_account_table', 1),
(16, '2023_03_16_111653_create_table_role', 2),
(17, '2023_03_17_140517_create_tagname_table', 3),
(18, '2023_03_17_140748_create_tagid_table', 4),
(19, '2023_03_17_141132_create_tagid_table', 5),
(20, '2023_03_17_141840_create_tagid_table', 6),
(21, '2023_03_21_121549_add_column_number_print', 7),
(22, '2023_03_21_160316_create_diary_table', 8);

-- --------------------------------------------------------

--
-- Table structure for table `number_print`
--

CREATE TABLE `number_print` (
  `id` bigint(20) UNSIGNED NOT NULL,
  `number_print` int(11) NOT NULL,
  `id_print` bigint(20) UNSIGNED NOT NULL,
  `user_id` bigint(20) UNSIGNED NOT NULL,
  `grant_time` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `expired` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `status` int(11) NOT NULL,
  `supply` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `created_at` timestamp NULL DEFAULT NULL,
  `updated_at` timestamp NULL DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

--
-- Dumping data for table `number_print`
--

INSERT INTO `number_print` (`id`, `number_print`, `id_print`, `user_id`, `grant_time`, `expired`, `status`, `supply`, `created_at`, `updated_at`) VALUES
(1, 2010000, 1, 1, '10:32 25-03-2023', '17:59 25-03-2023', 2, 'Kiosk', '2023-03-09 03:32:22', '2023-03-25 03:32:22'),
(2, 2010001, 1, 2, '10:32 25-03-2023', '17:59 25-03-2023', 1, 'Kiosk', '2023-03-09 03:32:33', '2023-03-25 03:32:33'),
(3, 3010000, 2, 1, '10:32 25-03-2023', '17:59 25-03-2023', 0, 'Kiosk', '2023-03-10 03:32:42', '2023-03-25 03:32:42'),
(4, 3010001, 2, 2, '10:32 25-03-2023', '17:59 25-03-2023', 1, 'Kiosk', '2023-03-11 03:32:55', '2023-03-25 03:32:55'),
(5, 3010002, 2, 4, '10:33 25-03-2023', '17:59 25-03-2023', 2, 'Kiosk', '2023-03-16 03:33:00', '2023-03-25 03:33:00'),
(6, 4010000, 3, 4, '10:33 25-03-2023', '17:59 25-03-2023', 1, 'Kiosk', '2023-03-25 03:33:04', '2023-03-25 03:33:04'),
(7, 2010002, 1, 5, '11:36 25-03-2023', '17:59 25-03-2023', 2, 'Kiosk', '2023-03-25 04:36:37', '2023-03-25 04:36:37'),
(8, 3010003, 2, 5, '11:36 25-03-2023', '17:59 25-03-2023', 1, 'Kiosk', '2023-03-25 04:36:52', '2023-03-25 04:36:52'),
(9, 4010001, 3, 6, '11:37 25-03-2023', '17:59 25-03-2023', 2, 'Kiosk', '2023-03-25 04:37:03', '2023-03-25 04:37:03'),
(10, 5010000, 4, 3, '11:37 25-03-2023', '17:59 25-03-2023', 1, 'Kiosk', '2023-03-25 04:37:12', '2023-03-25 04:37:12'),
(11, 5010001, 4, 5, '11:37 25-03-2023', '17:59 25-03-2023', 1, 'Kiosk', '2023-03-25 04:37:21', '2023-03-25 04:37:21');

-- --------------------------------------------------------

--
-- Table structure for table `ordinal`
--

CREATE TABLE `ordinal` (
  `id` bigint(20) UNSIGNED NOT NULL,
  `number` int(11) NOT NULL,
  `service_id` bigint(20) UNSIGNED NOT NULL,
  `is_printed` tinyint(1) NOT NULL DEFAULT 0,
  `status` tinyint(1) NOT NULL DEFAULT 0,
  `created_at` timestamp NULL DEFAULT NULL,
  `updated_at` timestamp NULL DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

--
-- Dumping data for table `ordinal`
--

INSERT INTO `ordinal` (`id`, `number`, `service_id`, `is_printed`, `status`, `created_at`, `updated_at`) VALUES
(1, 2010000, 1, 1, 2, '2023-03-09 02:50:45', '2023-03-25 02:50:45'),
(2, 2010001, 1, 1, 1, '2023-03-09 02:50:45', '2023-03-25 02:50:45'),
(3, 2010002, 1, 1, 1, '2023-03-09 02:50:45', '2023-03-25 02:50:45'),
(4, 2010003, 1, 0, 2, '2023-03-09 02:50:45', '2023-03-25 02:50:45'),
(5, 2010004, 1, 0, 0, '2023-03-15 02:50:45', '2023-03-25 02:50:45'),
(6, 2010005, 1, 0, 0, '2023-03-15 02:50:45', '2023-03-25 02:50:45'),
(7, 2010006, 1, 0, 1, '2023-03-15 02:50:45', '2023-03-25 02:50:45'),
(8, 2010007, 1, 0, 1, '2023-03-09 02:50:45', '2023-03-25 02:50:45'),
(9, 2010008, 1, 0, 1, '2023-03-12 02:50:45', '2023-03-25 02:50:45'),
(10, 2010009, 1, 0, 2, '2023-03-14 02:50:45', '2023-03-25 02:50:45'),
(11, 2010010, 1, 0, 2, '2023-03-25 02:50:45', '2023-03-25 02:50:45'),
(12, 2010011, 1, 0, 1, '2023-03-25 02:50:45', '2023-03-25 02:50:45'),
(13, 2010012, 1, 0, 1, '2023-03-25 02:50:45', '2023-03-25 02:50:45'),
(14, 2010013, 1, 0, 1, '2023-03-25 02:50:45', '2023-03-25 02:50:45'),
(15, 2010014, 1, 0, 1, '2023-03-25 02:50:45', '2023-03-25 02:50:45'),
(16, 2010015, 1, 0, 1, '2023-03-25 02:50:45', '2023-03-25 02:50:45'),
(17, 3010000, 2, 1, 0, '2023-03-09 02:54:11', '2023-03-25 02:54:11'),
(18, 3010001, 2, 1, 0, '2023-03-10 02:54:12', '2023-03-25 02:54:12'),
(19, 3010002, 2, 1, 2, '2023-03-11 02:54:12', '2023-03-25 02:54:12'),
(20, 3010003, 2, 1, 1, '2023-03-12 02:54:12', '2023-03-25 02:54:12'),
(21, 3010004, 2, 0, 2, '2023-03-13 02:54:12', '2023-03-25 02:54:12'),
(22, 3010005, 2, 0, 2, '2023-03-14 02:54:12', '2023-03-25 02:54:12'),
(23, 3010006, 2, 0, 1, '2023-03-15 02:54:12', '2023-03-25 02:54:12'),
(24, 3010007, 2, 0, 1, '2023-03-16 02:54:12', '2023-03-25 02:54:12'),
(25, 3010008, 2, 0, 0, '2023-03-25 02:54:12', '2023-03-25 02:54:12'),
(26, 3010009, 2, 0, 2, '2023-03-25 02:54:12', '2023-03-25 02:54:12'),
(27, 3010010, 2, 0, 2, '2023-03-25 02:54:12', '2023-03-25 02:54:12'),
(28, 3010011, 2, 0, 0, '2023-03-25 02:54:12', '2023-03-25 02:54:12'),
(29, 3010012, 2, 0, 2, '2023-03-25 02:54:12', '2023-03-25 02:54:12'),
(30, 3010013, 2, 0, 2, '2023-03-25 02:54:12', '2023-03-25 02:54:12'),
(31, 3010014, 2, 0, 2, '2023-03-25 02:54:12', '2023-03-25 02:54:12'),
(32, 3010015, 2, 0, 1, '2023-03-25 02:54:12', '2023-03-25 02:54:12'),
(33, 4010000, 3, 1, 0, '2023-03-25 02:54:34', '2023-03-25 02:54:34'),
(34, 4010001, 3, 1, 0, '2023-03-25 02:54:34', '2023-03-25 02:54:34'),
(35, 4010002, 3, 0, 2, '2023-03-25 02:54:34', '2023-03-25 02:54:34'),
(36, 4010003, 3, 0, 2, '2023-03-25 02:54:34', '2023-03-25 02:54:34'),
(37, 4010004, 3, 0, 2, '2023-03-25 02:54:34', '2023-03-25 02:54:34'),
(38, 4010005, 3, 0, 0, '2023-03-25 02:54:34', '2023-03-25 02:54:34'),
(39, 4010006, 3, 0, 1, '2023-03-25 02:54:34', '2023-03-25 02:54:34'),
(40, 4010007, 3, 0, 1, '2023-03-25 02:54:34', '2023-03-25 02:54:34'),
(41, 4010008, 3, 0, 2, '2023-03-25 02:54:34', '2023-03-25 02:54:34'),
(42, 4010009, 3, 0, 1, '2023-03-25 02:54:34', '2023-03-25 02:54:34'),
(43, 4010010, 3, 0, 2, '2023-03-25 02:54:34', '2023-03-25 02:54:34'),
(44, 4010011, 3, 0, 2, '2023-03-25 02:54:34', '2023-03-25 02:54:34'),
(45, 4010012, 3, 0, 0, '2023-03-25 02:54:34', '2023-03-25 02:54:34'),
(46, 4010013, 3, 0, 2, '2023-03-25 02:54:34', '2023-03-25 02:54:34'),
(47, 4010014, 3, 0, 1, '2023-03-25 02:54:34', '2023-03-25 02:54:34'),
(48, 4010015, 3, 0, 0, '2023-03-25 02:54:34', '2023-03-25 02:54:34'),
(49, 5010000, 4, 1, 1, '2023-03-25 04:30:51', '2023-03-25 04:30:51'),
(50, 5010001, 4, 1, 0, '2023-03-25 04:30:51', '2023-03-25 04:30:51'),
(51, 5010002, 4, 0, 0, '2023-03-25 04:30:51', '2023-03-25 04:30:51'),
(52, 5010003, 4, 0, 1, '2023-03-25 04:30:51', '2023-03-25 04:30:51'),
(53, 5010004, 4, 0, 0, '2023-03-25 04:30:51', '2023-03-25 04:30:51'),
(54, 5010005, 4, 0, 0, '2023-03-25 04:30:51', '2023-03-25 04:30:51'),
(55, 5010006, 4, 0, 2, '2023-03-25 04:30:51', '2023-03-25 04:30:51'),
(56, 5010007, 4, 0, 0, '2023-03-25 04:30:51', '2023-03-25 04:30:51'),
(57, 5010008, 4, 0, 1, '2023-03-25 04:30:51', '2023-03-25 04:30:51'),
(58, 5010009, 4, 0, 2, '2023-03-25 04:30:51', '2023-03-25 04:30:51'),
(59, 5010010, 4, 0, 0, '2023-03-25 04:30:52', '2023-03-25 04:30:52'),
(60, 5010011, 4, 0, 0, '2023-03-25 04:30:52', '2023-03-25 04:30:52'),
(61, 5010012, 4, 0, 0, '2023-03-25 04:30:52', '2023-03-25 04:30:52'),
(62, 5010013, 4, 0, 2, '2023-03-25 04:30:52', '2023-03-25 04:30:52'),
(63, 5010014, 4, 0, 0, '2023-03-25 04:30:52', '2023-03-25 04:30:52'),
(64, 5010015, 4, 0, 1, '2023-03-25 04:30:52', '2023-03-25 04:30:52');

-- --------------------------------------------------------

--
-- Table structure for table `password_resets`
--

CREATE TABLE `password_resets` (
  `email` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `token` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `created_at` timestamp NULL DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- --------------------------------------------------------

--
-- Table structure for table `personal_access_tokens`
--

CREATE TABLE `personal_access_tokens` (
  `id` bigint(20) UNSIGNED NOT NULL,
  `tokenable_type` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `tokenable_id` bigint(20) UNSIGNED NOT NULL,
  `name` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `token` varchar(64) COLLATE utf8_unicode_ci NOT NULL,
  `abilities` text COLLATE utf8_unicode_ci DEFAULT NULL,
  `last_used_at` timestamp NULL DEFAULT NULL,
  `expires_at` timestamp NULL DEFAULT NULL,
  `created_at` timestamp NULL DEFAULT NULL,
  `updated_at` timestamp NULL DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- --------------------------------------------------------

--
-- Table structure for table `role`
--

CREATE TABLE `role` (
  `id` bigint(20) UNSIGNED NOT NULL,
  `rolename` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `member` int(11) NOT NULL,
  `description` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `permission` tinyint(1) NOT NULL DEFAULT 0,
  `created_at` timestamp NULL DEFAULT NULL,
  `updated_at` timestamp NULL DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

--
-- Dumping data for table `role`
--

INSERT INTO `role` (`id`, `rolename`, `member`, `description`, `permission`, `created_at`, `updated_at`) VALUES
(8, 'Kế toán', 6, 'Thực hiện thống kê số liệu và tổng hợp số liệu', 0, '2023-03-16 05:32:08', '2023-03-16 06:03:22'),
(9, 'Bác sĩ', 6, 'Thực hiện thống kê số liệu và tổng hợp số liệu', 1, '2023-03-16 05:32:33', '2023-03-16 05:32:33'),
(10, 'Lễ Tân', 6, 'Thực hiện thống kê số liệu và tổng hợp số liệu', 0, '2023-03-16 05:32:51', '2023-03-16 05:32:51'),
(11, 'Quản lý', 6, 'Thực hiện thống kê số liệu và tổng hợp số liệu', 1, '2023-03-16 05:33:11', '2023-03-16 05:33:11'),
(12, 'Admin', 6, 'Thực hiện thống kê số liệu và tổng hợp số liệu', 0, '2023-03-16 05:33:26', '2023-03-16 05:33:26'),
(13, 'Superadmin', 6, 'Thực hiện thống kê số liệu và tổng hợp số liệu', 1, '2023-03-16 05:33:49', '2023-03-16 05:33:49'),
(14, 'Kế toán', 6, 'Kế toán', 1, '2023-03-25 04:40:48', '2023-03-25 04:40:48');

-- --------------------------------------------------------

--
-- Table structure for table `service`
--

CREATE TABLE `service` (
  `id` bigint(20) UNSIGNED NOT NULL,
  `servicecode` int(11) NOT NULL,
  `servicename` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `description` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `status` tinyint(1) NOT NULL DEFAULT 0,
  `created_at` timestamp NULL DEFAULT NULL,
  `updated_at` timestamp NULL DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

--
-- Dumping data for table `service`
--

INSERT INTO `service` (`id`, `servicecode`, `servicename`, `description`, `status`, `created_at`, `updated_at`) VALUES
(1, 201, 'Khám tim mạch', 'Chuyên khám tim mạch', 0, '2023-03-09 02:50:45', '2023-03-25 02:50:45'),
(2, 301, 'Khám sản - phụ khoa', 'Chuyên khám sản phụ khoa', 1, '2023-03-12 02:54:11', '2023-03-25 02:54:11'),
(3, 401, 'Khám hô hấp', 'Chuyên khám hô hấp', 1, '2023-03-14 02:54:34', '2023-03-25 04:33:28'),
(4, 501, 'Khám răng hàm mặt', 'Chuyên Khám răng hàm mặt', 0, '2023-03-25 04:30:51', '2023-03-25 04:30:51');

-- --------------------------------------------------------

--
-- Table structure for table `tagid`
--

CREATE TABLE `tagid` (
  `id` bigint(20) UNSIGNED NOT NULL,
  `user_id` bigint(20) UNSIGNED NOT NULL,
  `tag_id` bigint(20) UNSIGNED NOT NULL,
  `created_at` timestamp NULL DEFAULT NULL,
  `updated_at` timestamp NULL DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

--
-- Dumping data for table `tagid`
--

INSERT INTO `tagid` (`id`, `user_id`, `tag_id`, `created_at`, `updated_at`) VALUES
(1, 1, 1, '2023-03-17 07:22:01', '2023-03-17 07:22:01'),
(3, 2, 3, '2023-03-17 07:23:20', '2023-03-17 07:23:20'),
(4, 3, 4, '2023-03-17 10:11:44', '2023-03-17 10:11:44'),
(7, 5, 4, '2023-03-25 04:15:29', '2023-03-25 04:15:29'),
(9, 4, 3, '2023-03-25 04:16:22', '2023-03-25 04:16:22'),
(10, 6, 4, '2023-03-25 04:28:05', '2023-03-25 04:28:05'),
(11, 6, 3, '2023-03-25 04:28:05', '2023-03-25 04:28:05'),
(12, 1, 2, '2023-03-28 09:37:36', '2023-03-28 09:37:36');

-- --------------------------------------------------------

--
-- Table structure for table `tagname`
--

CREATE TABLE `tagname` (
  `id` bigint(20) UNSIGNED NOT NULL,
  `device_service` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `created_at` timestamp NULL DEFAULT NULL,
  `updated_at` timestamp NULL DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

--
-- Dumping data for table `tagname`
--

INSERT INTO `tagname` (`id`, `device_service`, `created_at`, `updated_at`) VALUES
(1, 'Khám tim mạch', '2023-03-17 07:22:01', '2023-03-17 07:22:01'),
(2, 'Khám sản phụ khoa', '2023-03-17 07:22:01', '2023-03-17 07:22:01'),
(3, 'Khám răng hàm mặt', '2023-03-17 07:23:20', '2023-03-17 07:23:20'),
(4, 'Khám tổng quát', '2023-03-17 10:11:44', '2023-03-17 10:11:44'),
(5, 'Khám hô hấp', '2023-03-25 03:49:22', '2023-03-25 03:49:22');

-- --------------------------------------------------------

--
-- Table structure for table `user`
--

CREATE TABLE `user` (
  `id` bigint(20) UNSIGNED NOT NULL,
  `username` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `loginname` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `password` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `phone` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `email` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `role` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `code` varchar(255) COLLATE utf8_unicode_ci DEFAULT NULL,
  `created_at` timestamp NULL DEFAULT NULL,
  `updated_at` timestamp NULL DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

--
-- Dumping data for table `user`
--

INSERT INTO `user` (`id`, `username`, `loginname`, `password`, `phone`, `email`, `role`, `code`, `created_at`, `updated_at`) VALUES
(3, 'Nguyễn Quốc Vũ', 'nguyenvu2002', '$2y$10$wKUtd1XL0FWgf9gizc3vyO.xdzJvDEcm1qfy71HpAiMuqQeSsQrdW', '0125349999', 'nguyenquocvu2002814@gmail.com', 'Kế Toán', 'STB1rPrrZNgDZxOJ', NULL, '2023-03-25 04:26:27'),
(4, 'Admin', 'admin', '$2y$10$Znf2ICwXshyL4FpPqDjY9O.rOUYxZZFIrC/pIphBaOG1A977fhf/y', '0125349999', 'nguyenquocvu2002814@gmail.com', 'Admin', NULL, NULL, NULL);

-- --------------------------------------------------------

--
-- Table structure for table `users`
--

CREATE TABLE `users` (
  `id` bigint(20) UNSIGNED NOT NULL,
  `name` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `email` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `email_verified_at` timestamp NULL DEFAULT NULL,
  `password` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `remember_token` varchar(100) COLLATE utf8_unicode_ci DEFAULT NULL,
  `created_at` timestamp NULL DEFAULT NULL,
  `updated_at` timestamp NULL DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

--
-- Indexes for dumped tables
--

--
-- Indexes for table `account`
--
ALTER TABLE `account`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `cumtomer_service`
--
ALTER TABLE `cumtomer_service`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `customer`
--
ALTER TABLE `customer`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `device`
--
ALTER TABLE `device`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `diary`
--
ALTER TABLE `diary`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `failed_jobs`
--
ALTER TABLE `failed_jobs`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `failed_jobs_uuid_unique` (`uuid`);

--
-- Indexes for table `migrations`
--
ALTER TABLE `migrations`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `number_print`
--
ALTER TABLE `number_print`
  ADD PRIMARY KEY (`id`),
  ADD KEY `number_print_id_print_foreign` (`id_print`),
  ADD KEY `number_print_user_id_foreign` (`user_id`);

--
-- Indexes for table `ordinal`
--
ALTER TABLE `ordinal`
  ADD PRIMARY KEY (`id`),
  ADD KEY `ordinal_service_id_foreign` (`service_id`);

--
-- Indexes for table `password_resets`
--
ALTER TABLE `password_resets`
  ADD PRIMARY KEY (`email`);

--
-- Indexes for table `personal_access_tokens`
--
ALTER TABLE `personal_access_tokens`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `personal_access_tokens_token_unique` (`token`),
  ADD KEY `personal_access_tokens_tokenable_type_tokenable_id_index` (`tokenable_type`,`tokenable_id`);

--
-- Indexes for table `role`
--
ALTER TABLE `role`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `service`
--
ALTER TABLE `service`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `tagid`
--
ALTER TABLE `tagid`
  ADD PRIMARY KEY (`id`),
  ADD KEY `tagid_user_id_foreign` (`user_id`),
  ADD KEY `tagid_tag_id_foreign` (`tag_id`);

--
-- Indexes for table `tagname`
--
ALTER TABLE `tagname`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `user`
--
ALTER TABLE `user`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `users_email_unique` (`email`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `account`
--
ALTER TABLE `account`
  MODIFY `id` bigint(20) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT for table `cumtomer_service`
--
ALTER TABLE `cumtomer_service`
  MODIFY `id` bigint(20) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=14;

--
-- AUTO_INCREMENT for table `customer`
--
ALTER TABLE `customer`
  MODIFY `id` bigint(20) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;

--
-- AUTO_INCREMENT for table `device`
--
ALTER TABLE `device`
  MODIFY `id` bigint(20) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=8;

--
-- AUTO_INCREMENT for table `diary`
--
ALTER TABLE `diary`
  MODIFY `id` bigint(20) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;

--
-- AUTO_INCREMENT for table `failed_jobs`
--
ALTER TABLE `failed_jobs`
  MODIFY `id` bigint(20) UNSIGNED NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `migrations`
--
ALTER TABLE `migrations`
  MODIFY `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=23;

--
-- AUTO_INCREMENT for table `number_print`
--
ALTER TABLE `number_print`
  MODIFY `id` bigint(20) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=12;

--
-- AUTO_INCREMENT for table `ordinal`
--
ALTER TABLE `ordinal`
  MODIFY `id` bigint(20) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=65;

--
-- AUTO_INCREMENT for table `personal_access_tokens`
--
ALTER TABLE `personal_access_tokens`
  MODIFY `id` bigint(20) UNSIGNED NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `role`
--
ALTER TABLE `role`
  MODIFY `id` bigint(20) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=15;

--
-- AUTO_INCREMENT for table `service`
--
ALTER TABLE `service`
  MODIFY `id` bigint(20) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT for table `tagid`
--
ALTER TABLE `tagid`
  MODIFY `id` bigint(20) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=13;

--
-- AUTO_INCREMENT for table `tagname`
--
ALTER TABLE `tagname`
  MODIFY `id` bigint(20) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT for table `user`
--
ALTER TABLE `user`
  MODIFY `id` bigint(20) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT for table `users`
--
ALTER TABLE `users`
  MODIFY `id` bigint(20) UNSIGNED NOT NULL AUTO_INCREMENT;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `number_print`
--
ALTER TABLE `number_print`
  ADD CONSTRAINT `number_print_id_print_foreign` FOREIGN KEY (`id_print`) REFERENCES `ordinal` (`id`) ON DELETE CASCADE,
  ADD CONSTRAINT `number_print_user_id_foreign` FOREIGN KEY (`user_id`) REFERENCES `customer` (`id`) ON DELETE CASCADE;

--
-- Constraints for table `ordinal`
--
ALTER TABLE `ordinal`
  ADD CONSTRAINT `ordinal_service_id_foreign` FOREIGN KEY (`service_id`) REFERENCES `service` (`id`) ON DELETE CASCADE;

--
-- Constraints for table `tagid`
--
ALTER TABLE `tagid`
  ADD CONSTRAINT `tagid_tag_id_foreign` FOREIGN KEY (`tag_id`) REFERENCES `tagname` (`id`) ON DELETE CASCADE,
  ADD CONSTRAINT `tagid_user_id_foreign` FOREIGN KEY (`user_id`) REFERENCES `device` (`id`) ON DELETE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
