CREATE TABLE `eventos` (
  `infEvento_nSeqEvento` int(11) NOT NULL,
  `NFe_infNFe_id` varchar(50) NOT NULL,
  `infEvento_tpEvento` int(6) NOT NULL,
  `infEvento_dhEvento` datetime DEFAULT NULL,
  `NFe_emit_CNPJ` varchar(45) NOT NULL,
  `infEvento_detEvento_xCorrecao` text,
  `retEvento_cStat` int(11) DEFAULT NULL,
  `retEvento_xMotivo` text,
  `statusEvento` int(3) DEFAULT NULL,
  PRIMARY KEY (`infEvento_nSeqEvento`,`NFe_infNFe_id`,`infEvento_tpEvento`),
  KEY `eventos_notas_idx1` (`NFe_emit_CNPJ`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8

