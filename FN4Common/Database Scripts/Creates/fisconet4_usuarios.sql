delimiter $$

CREATE TABLE `usuarios` (
  `idUsuario` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `email` varchar(40) NOT NULL,
  `senha` varchar(20) NOT NULL,
  `tipo` tinyint(4) NOT NULL,
  `nome` varchar(100) NOT NULL,
  PRIMARY KEY (`idUsuario`) USING BTREE,
  KEY `AI_id_usuario` (`idUsuario`) USING BTREE
) ENGINE=MyISAM AUTO_INCREMENT=14 DEFAULT CHARSET=utf8$$

