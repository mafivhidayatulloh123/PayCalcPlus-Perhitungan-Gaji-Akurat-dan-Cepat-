-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: May 09, 2025 at 04:24 AM
-- Server version: 10.4.32-MariaDB
-- PHP Version: 8.0.30

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `paycalcplus`
--

-- --------------------------------------------------------

--
-- Table structure for table `gaji`
--

CREATE TABLE `gaji` (
  `GajiID` int(11) NOT NULL,
  `Tanggal` date NOT NULL,
  `NIP` varchar(20) NOT NULL,
  `Jabatan` varchar(50) NOT NULL,
  `GajiPokok` decimal(15,2) NOT NULL,
  `Potongan` decimal(15,2) NOT NULL,
  `GajiBersih` decimal(15,2) GENERATED ALWAYS AS (`GajiPokok` - `Potongan`) STORED
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `gaji`
--

INSERT INTO `gaji` (`GajiID`, `Tanggal`, `NIP`, `Jabatan`, `GajiPokok`, `Potongan`) VALUES
(1, '2025-05-07', '2213020046', 'CEO', 20000000.00, 500000.00);

-- --------------------------------------------------------

--
-- Table structure for table `gaji_jabatan`
--

CREATE TABLE `gaji_jabatan` (
  `Jabatan` varchar(50) NOT NULL,
  `GajiPokok` decimal(15,2) NOT NULL,
  `Tunjangan` decimal(15,2) NOT NULL,
  `GajiBersih` decimal(15,2) GENERATED ALWAYS AS (`GajiPokok` + `Tunjangan`) STORED
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `gaji_jabatan`
--

INSERT INTO `gaji_jabatan` (`Jabatan`, `GajiPokok`, `Tunjangan`) VALUES
('Admin', 10000000.00, 2000000.00),
('CEO', 50000000.00, 20000000.00),
('CMO', 25000000.00, 10000000.00),
('IT Support', 19000000.00, 2000000.00),
('Manager', 30000000.00, 1500000.00),
('Marketing', 17000000.00, 8000000.00),
('Staff', 15000000.00, 5000000.00);

-- --------------------------------------------------------

--
-- Table structure for table `jabatan`
--

CREATE TABLE `jabatan` (
  `Jabatan` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `jabatan`
--

INSERT INTO `jabatan` (`Jabatan`) VALUES
('Admin'),
('CEO'),
('CMO'),
('IT Support'),
('Manager'),
('Marketing'),
('Staff');

-- --------------------------------------------------------

--
-- Table structure for table `karyawan`
--

CREATE TABLE `karyawan` (
  `KodeKaryawan` varchar(10) NOT NULL,
  `NIP` varchar(20) NOT NULL,
  `NamaKaryawan` varchar(100) NOT NULL,
  `Jabatan` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `karyawan`
--

INSERT INTO `karyawan` (`KodeKaryawan`, `NIP`, `NamaKaryawan`, `Jabatan`) VALUES
('K1', '2213020049', 'Julianto', 'Admin'),
('K12', '2213020046', 'Erwin Setiawan', 'CEO'),
('K13', '2213020045', 'Afiv Hidayatulloh', 'Marketing'),
('K15', '2213020020', 'Muhamad Fajar Sayuto', 'Manager'),
('K9', '2213020080', 'Tomi Setiawan', 'IT Support');

-- --------------------------------------------------------

--
-- Table structure for table `users`
--

CREATE TABLE `users` (
  `UserID` int(11) NOT NULL,
  `email` varchar(100) NOT NULL,
  `username` varchar(100) NOT NULL,
  `password` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `users`
--

INSERT INTO `users` (`UserID`, `email`, `username`, `password`) VALUES
(1, 'timi@gmail.com', 'timi', '123456'),
(2, 'apip', 'apip@gmail.com', '123456'),
(3, 'yumqo@gmail.com', 'yumqo', '123456');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `gaji`
--
ALTER TABLE `gaji`
  ADD PRIMARY KEY (`GajiID`),
  ADD KEY `NIP` (`NIP`);

--
-- Indexes for table `gaji_jabatan`
--
ALTER TABLE `gaji_jabatan`
  ADD PRIMARY KEY (`Jabatan`);

--
-- Indexes for table `jabatan`
--
ALTER TABLE `jabatan`
  ADD PRIMARY KEY (`Jabatan`);

--
-- Indexes for table `karyawan`
--
ALTER TABLE `karyawan`
  ADD PRIMARY KEY (`KodeKaryawan`),
  ADD UNIQUE KEY `NIP` (`NIP`);

--
-- Indexes for table `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`UserID`),
  ADD UNIQUE KEY `Email` (`email`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `gaji`
--
ALTER TABLE `gaji`
  MODIFY `GajiID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT for table `users`
--
ALTER TABLE `users`
  MODIFY `UserID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `gaji`
--
ALTER TABLE `gaji`
  ADD CONSTRAINT `gaji_ibfk_1` FOREIGN KEY (`NIP`) REFERENCES `karyawan` (`NIP`);

--
-- Constraints for table `gaji_jabatan`
--
ALTER TABLE `gaji_jabatan`
  ADD CONSTRAINT `gaji_jabatan_ibfk_1` FOREIGN KEY (`Jabatan`) REFERENCES `jabatan` (`Jabatan`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
