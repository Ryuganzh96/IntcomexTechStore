-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Servidor: 127.0.0.1
-- Tiempo de generación: 30-08-2024 a las 10:12:14
-- Versión del servidor: 10.4.32-MariaDB
-- Versión de PHP: 8.0.30

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de datos: `intcomex_db`
--

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `accesskeys`
--

CREATE TABLE `accesskeys` (
  `Id` int(11) NOT NULL,
  `Llave` varchar(255) NOT NULL,
  `Activa` enum('si','no') NOT NULL DEFAULT 'no',
  `FechaExpiracion` datetime NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_general_ci;

--
-- Volcado de datos para la tabla `accesskeys`
--

INSERT INTO `accesskeys` (`Id`, `Llave`, `Activa`, `FechaExpiracion`) VALUES
(1, 'F7gVb3P4R1N2kT5yWzX0aH', 'si', '2024-12-31 23:59:59');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `articulos`
--

CREATE TABLE `articulos` (
  `IdArticulo` int(11) NOT NULL,
  `SKU` varchar(50) NOT NULL,
  `MPN` varchar(50) NOT NULL,
  `Nombre` varchar(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_general_ci;

--
-- Volcado de datos para la tabla `articulos`
--

INSERT INTO `articulos` (`IdArticulo`, `SKU`, `MPN`, `Nombre`) VALUES
(1, 'SKU001', 'MPN001', 'Laptop XYZ'),
(2, '123456', 'MPN001', 'Laptop Acer Aspire 5'),
(3, '234567', 'MPN002', 'Laptop Dell Inspiron 15'),
(4, '345678', 'MPN003', 'PC HP All-in-One'),
(5, '456789', 'MPN004', 'iPhone 12'),
(6, '567890', 'MPN005', 'Samsung Galaxy S21'),
(7, '678901', 'MPN006', 'iPad Pro'),
(8, '789012', 'MPN007', 'Cámara Canon EOS Rebel'),
(9, '890123', 'MPN008', 'Router TP-Link'),
(10, '901234', 'MPN009', 'Switch Cisco'),
(11, '012345', 'MPN010', 'Tablet Samsung Galaxy Tab S7'),
(12, 'SKU77437', 'MPN77437', 'Bombillo inteligente');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `articulos_categoria`
--

CREATE TABLE `articulos_categoria` (
  `Id` int(11) NOT NULL,
  `Id_articulo` int(11) NOT NULL,
  `Id_categoria` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_general_ci;

--
-- Volcado de datos para la tabla `articulos_categoria`
--

INSERT INTO `articulos_categoria` (`Id`, `Id_articulo`, `Id_categoria`) VALUES
(1, 1, 1),
(2, 2, 1),
(3, 3, 1),
(4, 4, 1),
(5, 5, 3),
(6, 6, 3),
(7, 7, 4),
(8, 8, 5),
(9, 9, 6),
(10, 10, 6),
(11, 11, 4),
(12, 12, 2);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `articulos_subcategorias`
--

CREATE TABLE `articulos_subcategorias` (
  `Id` int(11) NOT NULL,
  `Id_Articulo` int(11) NOT NULL,
  `Id_Subcategoria` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_general_ci;

--
-- Volcado de datos para la tabla `articulos_subcategorias`
--

INSERT INTO `articulos_subcategorias` (`Id`, `Id_Articulo`, `Id_Subcategoria`) VALUES
(1, 1, 1),
(2, 1, 2),
(3, 5, 11),
(4, 5, 12),
(5, 5, 13),
(6, 5, 14),
(7, 6, 1),
(8, 6, 4),
(9, 8, 8),
(10, 8, 12),
(11, 8, 19),
(12, 8, 20),
(13, 8, 21),
(14, 8, 22);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `atributos`
--

CREATE TABLE `atributos` (
  `IdAtributo` int(11) NOT NULL,
  `NombreAtributo` varchar(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_general_ci;

--
-- Volcado de datos para la tabla `atributos`
--

INSERT INTO `atributos` (`IdAtributo`, `NombreAtributo`) VALUES
(1, 'Cantidad Memoria RAM'),
(2, 'Cantidad Memoria RAM'),
(3, 'Tamaño Disco Duro'),
(4, 'Procesador'),
(5, 'Cámara Principal'),
(6, 'Tamaño Pantalla'),
(7, 'Velocidad de Transmisión'),
(8, 'Número de Puertos'),
(9, 'Capacidad de Almacenamiento'),
(10, 'Resolución');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `categorias`
--

CREATE TABLE `categorias` (
  `IdCategoria` int(11) NOT NULL,
  `NombreCategoria` varchar(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_general_ci;

--
-- Volcado de datos para la tabla `categorias`
--

INSERT INTO `categorias` (`IdCategoria`, `NombreCategoria`) VALUES
(1, 'Computo'),
(2, 'Hogar inteligente'),
(3, 'Celulares'),
(4, 'Tablets'),
(5, 'Cámaras'),
(6, 'Redes');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `parametros`
--

CREATE TABLE `parametros` (
  `Id` int(11) NOT NULL,
  `Nombre` varchar(100) DEFAULT NULL,
  `Descripcion` varchar(255) DEFAULT NULL,
  `Valor` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_general_ci;

--
-- Volcado de datos para la tabla `parametros`
--

INSERT INTO `parametros` (`Id`, `Nombre`, `Descripcion`, `Valor`) VALUES
(1, 'Token_Duration', 'Duracion del token en segundos', '60'),
(2, 'Rate_Limit_Endpoint', 'Sirve para determinar a cuales endpoints afecta el rate limit, con * se cubren todos', '*'),
(3, 'Period', 'una vez alcanzado el limite de peticiones, en cuanto tiempo se puede volver a hacer consumos', '1m'),
(4, 'Limit', 'Cuántas solicitudes máximas se podrán enviar en un período', '100');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `subcategorias`
--

CREATE TABLE `subcategorias` (
  `IdSubcategoria` int(11) NOT NULL,
  `NombreSubcategoria` varchar(100) NOT NULL,
  `IdCategoria` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_general_ci;

--
-- Volcado de datos para la tabla `subcategorias`
--

INSERT INTO `subcategorias` (`IdSubcategoria`, `NombreSubcategoria`, `IdCategoria`) VALUES
(1, 'Gamer', 1),
(2, 'Laptops', 1),
(3, 'PC de Escritorio', 1),
(4, 'Smartphones', 3),
(5, 'Tablets Android', 4),
(6, 'Tablets iOS', 4),
(7, 'Cámaras Reflex', 5),
(8, 'Cámaras Digitales', 5),
(9, 'Routers', 6),
(10, 'Switches', 6),
(11, 'wifi', 3),
(12, 'Bluetooth', 3),
(13, 'Biometrico', 3),
(14, 'Gps', 3),
(19, 'Rafaga', 5),
(20, 'ISO', 5),
(21, 'Enfoque', 5),
(22, 'Estabilizacion', 5);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `valoresatributos`
--

CREATE TABLE `valoresatributos` (
  `IdValorAtributo` int(11) NOT NULL,
  `IdArticulo` int(11) NOT NULL,
  `IdAtributo` int(11) NOT NULL,
  `Valor` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_general_ci;

--
-- Volcado de datos para la tabla `valoresatributos`
--

INSERT INTO `valoresatributos` (`IdValorAtributo`, `IdArticulo`, `IdAtributo`, `Valor`) VALUES
(1, 1, 1, '8GB'),
(2, 1, 1, '8GB'),
(3, 1, 2, '512GB'),
(4, 1, 3, 'Intel i5'),
(5, 1, 5, '15.6 pulgadas'),
(6, 2, 1, '16GB'),
(7, 2, 2, '1TB'),
(8, 2, 3, 'Intel i7'),
(9, 2, 5, '15.6 pulgadas'),
(10, 3, 1, '8GB'),
(11, 3, 2, '1TB'),
(12, 3, 3, 'Intel i3'),
(13, 3, 5, '21.5 pulgadas'),
(14, 4, 4, '12MP'),
(15, 4, 5, '6.1 pulgadas'),
(16, 4, 8, '128GB'),
(17, 5, 4, '64MP'),
(18, 5, 5, '6.2 pulgadas'),
(19, 5, 8, '256GB'),
(20, 6, 1, '6GB'),
(21, 6, 5, '11 pulgadas'),
(22, 6, 8, '128GB'),
(23, 7, 4, '24.1MP'),
(24, 7, 9, '1920x1080'),
(25, 8, 6, '1Gbps'),
(26, 8, 7, '4'),
(27, 9, 7, '48'),
(28, 10, 1, '8GB'),
(29, 10, 5, '11 pulgadas'),
(30, 10, 8, '128GB');

--
-- Índices para tablas volcadas
--

--
-- Indices de la tabla `accesskeys`
--
ALTER TABLE `accesskeys`
  ADD PRIMARY KEY (`Id`);

--
-- Indices de la tabla `articulos`
--
ALTER TABLE `articulos`
  ADD PRIMARY KEY (`IdArticulo`);

--
-- Indices de la tabla `articulos_categoria`
--
ALTER TABLE `articulos_categoria`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `fk_articulo` (`Id_articulo`),
  ADD KEY `fk_categoria` (`Id_categoria`);

--
-- Indices de la tabla `articulos_subcategorias`
--
ALTER TABLE `articulos_subcategorias`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `articulos_subcategorias_ibfk_1` (`Id_Articulo`),
  ADD KEY `articulos_subcategorias_ibfk_2` (`Id_Subcategoria`);

--
-- Indices de la tabla `atributos`
--
ALTER TABLE `atributos`
  ADD PRIMARY KEY (`IdAtributo`);

--
-- Indices de la tabla `categorias`
--
ALTER TABLE `categorias`
  ADD PRIMARY KEY (`IdCategoria`);

--
-- Indices de la tabla `parametros`
--
ALTER TABLE `parametros`
  ADD PRIMARY KEY (`Id`);

--
-- Indices de la tabla `subcategorias`
--
ALTER TABLE `subcategorias`
  ADD PRIMARY KEY (`IdSubcategoria`),
  ADD KEY `IdCategoria` (`IdCategoria`);

--
-- Indices de la tabla `valoresatributos`
--
ALTER TABLE `valoresatributos`
  ADD PRIMARY KEY (`IdValorAtributo`),
  ADD KEY `IdArticulo` (`IdArticulo`),
  ADD KEY `IdAtributo` (`IdAtributo`);

--
-- AUTO_INCREMENT de las tablas volcadas
--

--
-- AUTO_INCREMENT de la tabla `accesskeys`
--
ALTER TABLE `accesskeys`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT de la tabla `articulos`
--
ALTER TABLE `articulos`
  MODIFY `IdArticulo` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=13;

--
-- AUTO_INCREMENT de la tabla `articulos_categoria`
--
ALTER TABLE `articulos_categoria`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=13;

--
-- AUTO_INCREMENT de la tabla `articulos_subcategorias`
--
ALTER TABLE `articulos_subcategorias`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=15;

--
-- AUTO_INCREMENT de la tabla `atributos`
--
ALTER TABLE `atributos`
  MODIFY `IdAtributo` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;

--
-- AUTO_INCREMENT de la tabla `categorias`
--
ALTER TABLE `categorias`
  MODIFY `IdCategoria` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;

--
-- AUTO_INCREMENT de la tabla `parametros`
--
ALTER TABLE `parametros`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT de la tabla `subcategorias`
--
ALTER TABLE `subcategorias`
  MODIFY `IdSubcategoria` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=23;

--
-- AUTO_INCREMENT de la tabla `valoresatributos`
--
ALTER TABLE `valoresatributos`
  MODIFY `IdValorAtributo` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=31;

--
-- Restricciones para tablas volcadas
--

--
-- Filtros para la tabla `articulos_categoria`
--
ALTER TABLE `articulos_categoria`
  ADD CONSTRAINT `fk_articulo` FOREIGN KEY (`Id_articulo`) REFERENCES `articulos` (`IdArticulo`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `fk_categoria` FOREIGN KEY (`Id_categoria`) REFERENCES `categorias` (`IdCategoria`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Filtros para la tabla `articulos_subcategorias`
--
ALTER TABLE `articulos_subcategorias`
  ADD CONSTRAINT `articulos_subcategorias_ibfk_1` FOREIGN KEY (`Id_Articulo`) REFERENCES `articulos` (`IdArticulo`),
  ADD CONSTRAINT `articulos_subcategorias_ibfk_2` FOREIGN KEY (`Id_Subcategoria`) REFERENCES `subcategorias` (`IdSubcategoria`);

--
-- Filtros para la tabla `subcategorias`
--
ALTER TABLE `subcategorias`
  ADD CONSTRAINT `subcategorias_ibfk_1` FOREIGN KEY (`IdCategoria`) REFERENCES `categorias` (`IdCategoria`);

--
-- Filtros para la tabla `valoresatributos`
--
ALTER TABLE `valoresatributos`
  ADD CONSTRAINT `valoresatributos_ibfk_1` FOREIGN KEY (`IdArticulo`) REFERENCES `articulos` (`IdArticulo`),
  ADD CONSTRAINT `valoresatributos_ibfk_2` FOREIGN KEY (`IdAtributo`) REFERENCES `atributos` (`IdAtributo`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
