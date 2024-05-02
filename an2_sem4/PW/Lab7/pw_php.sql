-- phpMyAdmin SQL Dump
-- version 5.2.0
-- https://www.phpmyadmin.net/
--
-- Gazdă: 127.0.0.1
-- Timp de generare: mai 16, 2023 la 02:36 PM
-- Versiune server: 10.4.24-MariaDB
-- Versiune PHP: 8.1.6

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Bază de date: `pw_php`
--

-- --------------------------------------------------------

--
-- Structură tabel pentru tabel `chestii`
--

CREATE TABLE `chestii` (
  `id` int(11) NOT NULL,
  `nume` varchar(50) NOT NULL,
  `tip` varchar(50) NOT NULL,
  `greutate` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Eliminarea datelor din tabel `chestii`
--

INSERT INTO `chestii` (`id`, `nume`, `tip`, `greutate`) VALUES
(1, 'a', 'b', 10),
(2, 'chestie0', 'mancare', 0),
(3, 'chestie1', 'electronice', 1),
(4, 'chestie2', 'carti', 3),
(5, 'chestie3', 'mancare', 6),
(6, 'chestie4', 'electronice', 10),
(7, 'chestie5', 'carti', 15),
(8, 'chestie6', 'mancare', 21),
(9, 'chestie7', 'electronice', 28),
(10, 'chestie8', 'carti', 36),
(11, 'chestie9', 'mancare', 45),
(12, 'chestie10', 'electronice', 55),
(13, 'chestie11', 'carti', 66),
(14, 'chestie12', 'mancare', 78),
(15, 'chestie13', 'electronice', 91),
(16, 'chestie14', 'carti', 105),
(17, 'chestie15', 'mancare', 120),
(18, 'chestie16', 'electronice', 136),
(19, 'chestie17', 'carti', 153),
(20, 'chestie18', 'mancare', 171),
(21, 'chestie19', 'electronice', 190),
(22, 'chestie20', 'carti', 210),
(23, 'chestie21', 'mancare', 231),
(24, 'chestie22', 'electronice', 253),
(25, 'chestie23', 'carti', 276),
(26, 'chestie24', 'mancare', 300),
(27, 'chestie25', 'electronice', 325),
(28, 'chestie26', 'carti', 351),
(29, 'chestie27', 'mancare', 378),
(30, 'chestie28', 'electronice', 406),
(31, 'chestie29', 'carti', 435),
(32, 'chestie30', 'mancare', 465),
(33, 'chestie31', 'electronice', 496),
(34, 'chestie32', 'carti', 528),
(35, 'chestie33', 'mancare', 561),
(36, 'chestie34', 'electronice', 595),
(37, 'chestie35', 'carti', 630),
(38, 'chestie36', 'mancare', 666),
(39, 'chestie37', 'electronice', 703),
(40, 'chestie38', 'carti', 741),
(41, 'chestie39', 'mancare', 780),
(42, 'chestie40', 'electronice', 820),
(43, 'chestie41', 'carti', 861),
(44, 'chestie42', 'mancare', 903),
(45, 'chestie43', 'electronice', 946),
(46, 'chestie44', 'carti', 990),
(47, 'chestie45', 'mancare', 1035),
(48, 'chestie46', 'electronice', 1081),
(49, 'chestie47', 'carti', 1128),
(50, 'chestie48', 'mancare', 1176),
(51, 'chestie49', 'electronice', 1225),
(52, 'chestie50', 'carti', 1275),
(53, 'chestie51', 'mancare', 1326),
(54, 'chestie52', 'electronice', 1378),
(55, 'chestie53', 'carti', 1431),
(56, 'chestie54', 'mancare', 1485),
(57, 'chestie55', 'electronice', 1540),
(58, 'chestie56', 'carti', 1596),
(59, 'chestie57', 'mancare', 1653),
(60, 'chestie58', 'electronice', 1711),
(61, 'chestie59', 'carti', 1770),
(62, 'chestie60', 'mancare', 1830),
(63, 'chestie61', 'electronice', 1891),
(64, 'chestie62', 'carti', 1953),
(65, 'chestie63', 'mancare', 2016),
(66, 'chestie64', 'electronice', 2080),
(67, 'chestie65', 'carti', 2145),
(68, 'chestie66', 'mancare', 2211),
(69, 'chestie67', 'electronice', 2278),
(70, 'chestie68', 'carti', 2346),
(71, 'chestie69', 'mancare', 2415),
(72, 'chestie70', 'electronice', 2485),
(73, 'chestie71', 'carti', 2556),
(74, 'chestie72', 'mancare', 2628),
(75, 'chestie73', 'electronice', 2701),
(76, 'chestie74', 'carti', 2775),
(77, 'chestie75', 'mancare', 2850),
(78, 'chestie76', 'electronice', 2926),
(79, 'chestie77', 'carti', 3003),
(80, 'chestie78', 'mancare', 3081),
(81, 'chestie79', 'electronice', 3160),
(82, 'chestie80', 'carti', 3240),
(83, 'chestie81', 'mancare', 3321),
(84, 'chestie82', 'electronice', 3403),
(85, 'chestie83', 'carti', 3486),
(86, 'chestie84', 'mancare', 3570),
(87, 'chestie85', 'electronice', 3655),
(88, 'chestie86', 'carti', 3741),
(89, 'chestie87', 'mancare', 3828),
(90, 'chestie88', 'electronice', 3916),
(91, 'chestie89', 'carti', 4005),
(92, 'chestie90', 'mancare', 4095),
(93, 'chestie91', 'electronice', 4186),
(94, 'chestie92', 'carti', 4278),
(95, 'chestie93', 'mancare', 4371),
(96, 'chestie94', 'electronice', 4465),
(97, 'chestie95', 'carti', 4560),
(98, 'chestie96', 'mancare', 4656),
(99, 'chestie97', 'electronice', 4753),
(100, 'chestie98', 'carti', 4851),
(101, 'chestie99', 'mancare', 4950),
(102, 'chestie100', 'electronice', 5050),
(103, 'chestie101', 'carti', 5151),
(104, 'chestie102', 'mancare', 5253),
(105, 'chestie103', 'electronice', 5356),
(106, 'chestie104', 'carti', 5460),
(107, 'chestie105', 'mancare', 5565),
(108, 'chestie106', 'electronice', 5671),
(109, 'chestie107', 'carti', 5778),
(110, 'chestie108', 'mancare', 5886),
(111, 'chestie109', 'electronice', 5995),
(112, 'chestie110', 'carti', 6105),
(113, 'chestie111', 'mancare', 6216),
(114, 'chestie112', 'electronice', 6328),
(115, 'chestie113', 'carti', 6441),
(116, 'chestie114', 'mancare', 6555),
(117, 'chestie115', 'electronice', 6670),
(118, 'chestie116', 'carti', 6786),
(119, 'chestie117', 'mancare', 6903),
(120, 'chestie118', 'electronice', 7021),
(121, 'chestie119', 'carti', 7140),
(122, 'chestie120', 'mancare', 7260),
(123, 'chestie121', 'electronice', 7381),
(124, 'chestie122', 'carti', 7503),
(125, 'chestie123', 'mancare', 7626),
(126, 'chestie124', 'electronice', 7750),
(127, 'chestie125', 'carti', 7875),
(128, 'chestie126', 'mancare', 8001),
(129, 'chestie127', 'electronice', 8128),
(130, 'chestie128', 'carti', 8256),
(131, 'chestie129', 'mancare', 8385),
(132, 'chestie130', 'electronice', 8515),
(133, 'chestie131', 'carti', 8646),
(134, 'chestie132', 'mancare', 8778),
(135, 'chestie133', 'electronice', 8911),
(136, 'chestie134', 'carti', 9045),
(137, 'chestie135', 'mancare', 9180),
(138, 'chestie136', 'electronice', 9316),
(139, 'chestie137', 'carti', 9453),
(140, 'chestie138', 'mancare', 9591),
(141, 'chestie139', 'electronice', 9730),
(142, 'chestie140', 'carti', 9870),
(143, 'chestie141', 'mancare', 10011),
(144, 'chestie142', 'electronice', 10153),
(145, 'chestie143', 'carti', 10296),
(146, 'chestie144', 'mancare', 10440),
(147, 'chestie145', 'electronice', 10585),
(148, 'chestie146', 'carti', 10731),
(149, 'chestie147', 'mancare', 10878),
(150, 'chestie148', 'electronice', 11026),
(151, 'chestie149', 'carti', 11175),
(152, 'chestie150', 'mancare', 11325),
(153, 'chestie151', 'electronice', 11476),
(154, 'chestie152', 'carti', 11628),
(155, 'chestie153', 'mancare', 11781),
(156, 'chestie154', 'electronice', 11935),
(157, 'chestie155', 'carti', 12090),
(158, 'chestie156', 'mancare', 12246),
(159, 'chestie157', 'electronice', 12403),
(160, 'chestie158', 'carti', 12561),
(161, 'chestie159', 'mancare', 12720),
(162, 'chestie160', 'electronice', 12880),
(163, 'chestie161', 'carti', 13041),
(164, 'chestie162', 'mancare', 13203),
(165, 'chestie163', 'electronice', 13366),
(166, 'chestie164', 'carti', 13530),
(167, 'chestie165', 'mancare', 13695),
(168, 'chestie166', 'electronice', 13861),
(169, 'chestie167', 'carti', 14028),
(170, 'chestie168', 'mancare', 14196),
(171, 'chestie169', 'electronice', 14365),
(172, 'chestie170', 'carti', 14535),
(173, 'chestie171', 'mancare', 14706),
(174, 'chestie172', 'electronice', 14878),
(175, 'chestie173', 'carti', 15051),
(176, 'chestie174', 'mancare', 15225),
(177, 'chestie175', 'electronice', 15400),
(178, 'chestie176', 'carti', 15576),
(179, 'chestie177', 'mancare', 15753),
(180, 'chestie178', 'electronice', 15931),
(181, 'chestie179', 'carti', 16110),
(182, 'chestie180', 'mancare', 16290),
(183, 'chestie181', 'electronice', 16471),
(184, 'chestie182', 'carti', 16653),
(185, 'chestie183', 'mancare', 16836),
(186, 'chestie184', 'electronice', 17020),
(187, 'chestie185', 'carti', 17205),
(188, 'chestie186', 'mancare', 17391),
(189, 'chestie187', 'electronice', 17578),
(190, 'chestie188', 'carti', 17766),
(191, 'chestie189', 'mancare', 17955),
(192, 'chestie190', 'electronice', 18145),
(193, 'chestie191', 'carti', 18336),
(194, 'chestie192', 'mancare', 18528),
(195, 'chestie193', 'electronice', 18721),
(196, 'chestie194', 'carti', 18915),
(197, 'chestie195', 'mancare', 19110),
(198, 'chestie196', 'electronice', 19306),
(199, 'chestie197', 'carti', 19503),
(200, 'chestie198', 'mancare', 19701),
(201, 'chestie199', 'electronice', 19900);

-- --------------------------------------------------------

--
-- Structură tabel pentru tabel `comentarii`
--

CREATE TABLE `comentarii` (
  `id` int(11) NOT NULL,
  `nume` varchar(100) NOT NULL,
  `text` varchar(1000) NOT NULL,
  `valid` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Eliminarea datelor din tabel `comentarii`
--

INSERT INTO `comentarii` (`id`, `nume`, `text`, `valid`) VALUES
(1, 'Marian', 'Viteaz omul', 1),
(2, 'vlad', 'mkmk', 1),
(3, 'Altcineva', 'xaxa', 1),
(4, 'Altcinevajj', 'xaxabhb', 1);

-- --------------------------------------------------------

--
-- Structură tabel pentru tabel `materii`
--

CREATE TABLE `materii` (
  `id` int(11) NOT NULL,
  `nume` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Eliminarea datelor din tabel `materii`
--

INSERT INTO `materii` (`id`, `nume`) VALUES
(1, 'PW'),
(2, 'SGBD');

-- --------------------------------------------------------

--
-- Structură tabel pentru tabel `note`
--

CREATE TABLE `note` (
  `idStudent` int(11) NOT NULL,
  `idMaterie` int(11) NOT NULL,
  `valoare` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Eliminarea datelor din tabel `note`
--

INSERT INTO `note` (`idStudent`, `idMaterie`, `valoare`) VALUES
(1, 1, 10),
(1, 2, 10),
(2, 2, 10);

-- --------------------------------------------------------

--
-- Structură tabel pentru tabel `profi`
--

CREATE TABLE `profi` (
  `user` varchar(100) NOT NULL,
  `pass` varchar(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Eliminarea datelor din tabel `profi`
--

INSERT INTO `profi` (`user`, `pass`) VALUES
('user1', '24c9e15e52afc47c225b757e7bee1f9d');

-- --------------------------------------------------------

--
-- Structură tabel pentru tabel `profi2`
--

CREATE TABLE `profi2` (
  `user` varchar(100) NOT NULL,
  `pass` varchar(100) NOT NULL,
  `pp64` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Eliminarea datelor din tabel `profi2`
--

INSERT INTO `profi2` (`user`, `pass`, `pp64`) VALUES
('user1', '24c9e15e52afc47c225b757e7bee1f9d', 'data:image/jpg;base64,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'),
('user2', '7e58d63b60197ceb55a1c487989a3720', 'data:image/jpg;base64,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');

-- --------------------------------------------------------

--
-- Structură tabel pentru tabel `studenti`
--

CREATE TABLE `studenti` (
  `id` int(11) NOT NULL,
  `nume` varchar(50) NOT NULL,
  `prenume` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Eliminarea datelor din tabel `studenti`
--

INSERT INTO `studenti` (`id`, `nume`, `prenume`) VALUES
(1, 'Vasile', 'Ioan'),
(2, 'Andreea', 'Dascalu'),
(3, 'Victor', 'Paraschiv'),
(4, 'Elena', 'Cristescu');

--
-- Indexuri pentru tabele eliminate
--

--
-- Indexuri pentru tabele `chestii`
--
ALTER TABLE `chestii`
  ADD PRIMARY KEY (`id`);

--
-- Indexuri pentru tabele `comentarii`
--
ALTER TABLE `comentarii`
  ADD PRIMARY KEY (`id`);

--
-- Indexuri pentru tabele `materii`
--
ALTER TABLE `materii`
  ADD PRIMARY KEY (`id`);

--
-- Indexuri pentru tabele `studenti`
--
ALTER TABLE `studenti`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT pentru tabele eliminate
--

--
-- AUTO_INCREMENT pentru tabele `chestii`
--
ALTER TABLE `chestii`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=202;

--
-- AUTO_INCREMENT pentru tabele `comentarii`
--
ALTER TABLE `comentarii`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT pentru tabele `materii`
--
ALTER TABLE `materii`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT pentru tabele `studenti`
--
ALTER TABLE `studenti`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
