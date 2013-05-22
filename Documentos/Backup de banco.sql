-- MySQL Administrator dump 1.4
--
-- ------------------------------------------------------
-- Server version	5.0.51a-community-nt


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;


--
-- Create schema fisconet4
--

CREATE DATABASE IF NOT EXISTS fisconet4;
USE fisconet4;

--
-- Definition of table `historico`
--

DROP TABLE IF EXISTS `historico`;
CREATE TABLE `historico` (
  `idHistorico` int(10) unsigned NOT NULL auto_increment,
  `NFe_ide_nNF` int(9) unsigned NOT NULL default '0',
  `dataHora` timestamp NOT NULL default CURRENT_TIMESTAMP,
  `complemento` text,
  `idTpHistorico` int(10) unsigned NOT NULL,
  PRIMARY KEY  USING BTREE (`idHistorico`),
  KEY `FK_historico_1` (`idTpHistorico`),
  KEY `FK_historico_2` (`NFe_ide_nNF`),
  CONSTRAINT `FK_historico_1` FOREIGN KEY (`idTpHistorico`) REFERENCES `tipodehistorico` (`idTpHistorico`),
  CONSTRAINT `FK_historico_2` FOREIGN KEY (`NFe_ide_nNF`) REFERENCES `notas` (`NFe_ide_nNF`)
) ENGINE=InnoDB AUTO_INCREMENT=112 DEFAULT CHARSET=utf8 ROW_FORMAT=DYNAMIC;

--
-- Dumping data for table `historico`
--

/*!40000 ALTER TABLE `historico` DISABLE KEYS */;
INSERT INTO `historico` (`idHistorico`,`NFe_ide_nNF`,`dataHora`,`complemento`,`idTpHistorico`) VALUES 
 (39,27,'2009-03-04 16:17:46','Tentativa n. 1',1),
 (40,27,'2009-03-04 16:17:48','',3),
 (41,27,'2009-03-04 16:19:42','Erro(linha 3 posição 25): O atributo \'Id\' é inválido - O valor \'\' é inválido dependendo do tipo de dados \'http://www.w3.org/2001/XMLSchema:ID\' - A seqüência vazia \'\' não é um nome válido.\r\n---\r\nErro(linha 12 posição 29): O elemento \'http://www.portalfiscal.inf.br/nfe:dEmi\' é inválido - O valor \'2008-0asda4-01\' é inválido dependendo do tipo de dados \'http://www.portalfiscal.inf.br/nfe:TData\' - Falha na restrição Pattern.\r\n---\r\nErro(linha 18 posição 14): O elemento \'http://www.portalfiscal.inf.br/nfe:cDV\' é inválido - O valor \'\' é inválido dependendo do tipo de dados \'String\' - Falha na restrição Pattern.\r\n---\r\nErro(linha 61 posição 61): O elemento \'http://www.portalfiscal.inf.br/nfe:cEAN\' é inválido - O valor \'OLEO BIODIESEL B2 INTERIOR-ONU 1202-CLASSE 3\' é inválido dependendo do tipo de dados \'String\' - Falha na restrição Pattern.\r\n---\r\nErro(linha 62 posição 18): O elemento \'http://www.portalfiscal.inf.br/nfe:xProd\' é inválido - O valor \'\' é inválido dependendo do tipo de dados \'String\' - Falha na restrição Pattern.\r\n---\r\nErro(linha 63 posição 23): O elemento \'http://www.portalfiscal.inf.br/nfe:genero\' é inválido - O valor \'5656\' é inválido dependendo do tipo de dados \'String\' - Falha na restrição Pattern.\r\n---\r\nErro(linha 64 posição 19): O elemento \'http://www.portalfiscal.inf.br/nfe:CFOP\' é inválido - O valor \'LT\' é inválido dependendo do tipo de dados \'http://www.portalfiscal.inf.br/nfe:TCfop\' - Falha na restrição Enumeration.\r\n---\r\nErro(linha 65 posição 34): O elemento \'http://www.portalfiscal.inf.br/nfe:uCom\' é inválido - O valor \'000000010000.0000\' é inválido dependendo do tipo de dados \'String\' - O comprimento real é maior do que o valor de Maxlength.\r\n---\r\nErro(linha 68 posição 21): O elemento \'http://www.portalfiscal.inf.br/nfe:vProd\' é inválido - O valor \'NaN\' é inválido dependendo do tipo de dados \'http://www.portalfiscal.inf.br/nfe:TDec_1302\' - Falha na restrição Pattern.\r\n---\r\nErro(linha 69 posição 65): O elemento \'http://www.portalfiscal.inf.br/nfe:cEANTrib\' é inválido - O valor \'OLEO BIODIESEL B2 INTERIOR-ONU 1202-CLASSE 3\' é inválido dependendo do tipo de dados \'String\' - Falha na restrição Pattern.\r\n---\r\nErro(linha 70 posição 39): O elemento \'http://www.portalfiscal.inf.br/nfe:uTrib\' é inválido - O valor \'0000000000000000.0000\' é inválido dependendo do tipo de dados \'String\' - O comprimento real é maior do que o valor de Maxlength.\r\n---\r\nErro(linha 72 posição 23): O elemento \'http://www.portalfiscal.inf.br/nfe:vUnTrib\' é inválido - O valor \'NaN\' é inválido dependendo do tipo de dados \'http://www.portalfiscal.inf.br/nfe:TDec_1204\' - Falha na restrição Pattern.\r\n---\r\nErro(linha 73 posição 18): O elemento \'http://www.portalfiscal.inf.br/nfe:vDesc\' é inválido - O valor \'\' é inválido dependendo do tipo de dados \'http://www.portalfiscal.inf.br/nfe:TDec_1302Opc\' - Falha na restrição Pattern.\r\n---\r\nErro(linha 78 posição 39): O elemento \'http://www.portalfiscal.inf.br/nfe:orig\' é inválido - O valor \'000000000000000.00\' é inválido dependendo do tipo de dados \'http://www.portalfiscal.inf.br/nfe:Torig\' - Falha na restrição Enumeration.\r\n---\r\nErro(linha 79 posição 22): O elemento \'http://www.portalfiscal.inf.br/nfe:CST\' é inválido - O valor \'00\' é inválido dependendo do tipo de dados \'String\' - Falha na restrição Enumeration.\r\n---\r\nErro(linha 87 posição 20): O elemento \'http://www.portalfiscal.inf.br/nfe:CST\' é inválido - O valor \'\' é inválido dependendo do tipo de dados \'String\' - Falha na restrição Enumeration.\r\n---\r\nErro(linha 90 posição 9): O elemento \'imposto\' no espaço para nome \'http://www.portalfiscal.inf.br/nfe\' apresenta conteúdo incompleto. Lista de possíveis elementos esperados: \'II, PIS\' no espaço para nome \'http://www.portalfiscal.inf.br/nfe\'.\r\n---\r\nErro(linha 111 posição 19): O elemento \'http://www.portalfiscal.inf.br/nfe:modFrete\' é inválido - O valor \'\' é inválido dependendo do tipo de dados \'String\' - Falha na restrição Enumeration.\r\n---\r\nErro(linha 113 posição 50): O elemento \'http://www.portalfiscal.inf.br/nfe:CPF\' é inválido - O valor \'ATADIESEL COM. DIESEL E LUBR. LTDA\' é inválido dependendo do tipo de dados \'http://www.portalfiscal.inf.br/nfe:TCpf\' - Falha na restrição Pattern.\r\n---\r\nErro(linha 114 posição 18): O elemento \'http://www.portalfiscal.inf.br/nfe:xNome\' é inválido - O valor \'\' é inválido dependendo do tipo de dados \'String\' - Falha na restrição Pattern.\r\n---\r\nErro(linha 116 posição 33): O elemento \'http://www.portalfiscal.inf.br/nfe:UF\' é inválido - O valor \'000000000000000.00\' é inválido dependendo do tipo de dados \'http://www.portalfiscal.inf.br/nfe:TUf\' - Falha na restrição Enumeration.\r\n---\r\nErro(linha 120 posição 15): O elemento \'http://www.portalfiscal.inf.br/nfe:UF\' é inválido - O valor \'\' é inválido dependendo do tipo de dados \'http://www.portalfiscal.inf.br/nfe:TUf\' - Falha na restrição Enumeration.\r\n---\r\nErro(linha 131 posição 20): O elemento \'http://www.portalfiscal.inf.br/nfe:vLiq\' é inválido - O valor \'NaN\' é inválido dependendo do tipo de dados \'http://www.portalfiscal.inf.br/nfe:TDec_1302Opc\' - Falha na restrição Pattern.\r\n---\r\nErro(linha 143 posição 3): O elemento \'NFe\' no espaço para nome \'http://www.portalfiscal.inf.br/nfe\' apresenta conteúdo incompleto. Lista de possíveis elementos esperados: \'Signature\' no espaço para nome \'http://www.w3.org/2000/09/xmldsig#\'.\r\n---\r\n',9),
 (42,27,'2009-03-04 16:32:54','Tentativa n. 2',1),
 (43,27,'2009-03-04 16:32:56','',3),
 (44,27,'2009-03-04 16:33:13','Erro(linha 3 posição 25): O atributo \'Id\' é inválido - O valor \'\' é inválido dependendo do tipo de dados \'http://www.w3.org/2001/XMLSchema:ID\' - A seqüência vazia \'\' não é um nome válido.\r\n---\r\nErro(linha 12 posição 29): O elemento \'http://www.portalfiscal.inf.br/nfe:dEmi\' é inválido - O valor \'2008-0asda4-01\' é inválido dependendo do tipo de dados \'http://www.portalfiscal.inf.br/nfe:TData\' - Falha na restrição Pattern.\r\n---\r\nErro(linha 18 posição 14): O elemento \'http://www.portalfiscal.inf.br/nfe:cDV\' é inválido - O valor \'\' é inválido dependendo do tipo de dados \'String\' - Falha na restrição Pattern.\r\n---\r\nErro(linha 61 posição 61): O elemento \'http://www.portalfiscal.inf.br/nfe:cEAN\' é inválido - O valor \'OLEO BIODIESEL B2 INTERIOR-ONU 1202-CLASSE 3\' é inválido dependendo do tipo de dados \'String\' - Falha na restrição Pattern.\r\n---\r\nErro(linha 62 posição 18): O elemento \'http://www.portalfiscal.inf.br/nfe:xProd\' é inválido - O valor \'\' é inválido dependendo do tipo de dados \'String\' - Falha na restrição Pattern.\r\n---\r\nErro(linha 63 posição 23): O elemento \'http://www.portalfiscal.inf.br/nfe:genero\' é inválido - O valor \'5656\' é inválido dependendo do tipo de dados \'String\' - Falha na restrição Pattern.\r\n---\r\nErro(linha 64 posição 19): O elemento \'http://www.portalfiscal.inf.br/nfe:CFOP\' é inválido - O valor \'LT\' é inválido dependendo do tipo de dados \'http://www.portalfiscal.inf.br/nfe:TCfop\' - Falha na restrição Enumeration.\r\n---\r\nErro(linha 65 posição 34): O elemento \'http://www.portalfiscal.inf.br/nfe:uCom\' é inválido - O valor \'000000010000.0000\' é inválido dependendo do tipo de dados \'String\' - O comprimento real é maior do que o valor de Maxlength.\r\n---\r\nErro(linha 68 posição 21): O elemento \'http://www.portalfiscal.inf.br/nfe:vProd\' é inválido - O valor \'NaN\' é inválido dependendo do tipo de dados \'http://www.portalfiscal.inf.br/nfe:TDec_1302\' - Falha na restrição Pattern.\r\n---\r\nErro(linha 69 posição 65): O elemento \'http://www.portalfiscal.inf.br/nfe:cEANTrib\' é inválido - O valor \'OLEO BIODIESEL B2 INTERIOR-ONU 1202-CLASSE 3\' é inválido dependendo do tipo de dados \'String\' - Falha na restrição Pattern.\r\n---\r\nErro(linha 70 posição 39): O elemento \'http://www.portalfiscal.inf.br/nfe:uTrib\' é inválido - O valor \'0000000000000000.0000\' é inválido dependendo do tipo de dados \'String\' - O comprimento real é maior do que o valor de Maxlength.\r\n---\r\nErro(linha 72 posição 23): O elemento \'http://www.portalfiscal.inf.br/nfe:vUnTrib\' é inválido - O valor \'NaN\' é inválido dependendo do tipo de dados \'http://www.portalfiscal.inf.br/nfe:TDec_1204\' - Falha na restrição Pattern.\r\n---\r\nErro(linha 73 posição 18): O elemento \'http://www.portalfiscal.inf.br/nfe:vDesc\' é inválido - O valor \'\' é inválido dependendo do tipo de dados \'http://www.portalfiscal.inf.br/nfe:TDec_1302Opc\' - Falha na restrição Pattern.\r\n---\r\nErro(linha 78 posição 39): O elemento \'http://www.portalfiscal.inf.br/nfe:orig\' é inválido - O valor \'000000000000000.00\' é inválido dependendo do tipo de dados \'http://www.portalfiscal.inf.br/nfe:Torig\' - Falha na restrição Enumeration.\r\n---\r\nErro(linha 79 posição 22): O elemento \'http://www.portalfiscal.inf.br/nfe:CST\' é inválido - O valor \'00\' é inválido dependendo do tipo de dados \'String\' - Falha na restrição Enumeration.\r\n---\r\nErro(linha 87 posição 20): O elemento \'http://www.portalfiscal.inf.br/nfe:CST\' é inválido - O valor \'\' é inválido dependendo do tipo de dados \'String\' - Falha na restrição Enumeration.\r\n---\r\nErro(linha 90 posição 9): O elemento \'imposto\' no espaço para nome \'http://www.portalfiscal.inf.br/nfe\' apresenta conteúdo incompleto. Lista de possíveis elementos esperados: \'II, PIS\' no espaço para nome \'http://www.portalfiscal.inf.br/nfe\'.\r\n---\r\nErro(linha 111 posição 19): O elemento \'http://www.portalfiscal.inf.br/nfe:modFrete\' é inválido - O valor \'\' é inválido dependendo do tipo de dados \'String\' - Falha na restrição Enumeration.\r\n---\r\nErro(linha 113 posição 50): O elemento \'http://www.portalfiscal.inf.br/nfe:CPF\' é inválido - O valor \'ATADIESEL COM. DIESEL E LUBR. LTDA\' é inválido dependendo do tipo de dados \'http://www.portalfiscal.inf.br/nfe:TCpf\' - Falha na restrição Pattern.\r\n---\r\nErro(linha 114 posição 18): O elemento \'http://www.portalfiscal.inf.br/nfe:xNome\' é inválido - O valor \'\' é inválido dependendo do tipo de dados \'String\' - Falha na restrição Pattern.\r\n---\r\nErro(linha 116 posição 33): O elemento \'http://www.portalfiscal.inf.br/nfe:UF\' é inválido - O valor \'000000000000000.00\' é inválido dependendo do tipo de dados \'http://www.portalfiscal.inf.br/nfe:TUf\' - Falha na restrição Enumeration.\r\n---\r\nErro(linha 120 posição 15): O elemento \'http://www.portalfiscal.inf.br/nfe:UF\' é inválido - O valor \'\' é inválido dependendo do tipo de dados \'http://www.portalfiscal.inf.br/nfe:TUf\' - Falha na restrição Enumeration.\r\n---\r\nErro(linha 131 posição 20): O elemento \'http://www.portalfiscal.inf.br/nfe:vLiq\' é inválido - O valor \'NaN\' é inválido dependendo do tipo de dados \'http://www.portalfiscal.inf.br/nfe:TDec_1302Opc\' - Falha na restrição Pattern.\r\n---\r\nErro(linha 143 posição 3): O elemento \'NFe\' no espaço para nome \'http://www.portalfiscal.inf.br/nfe\' apresenta conteúdo incompleto. Lista de possíveis elementos esperados: \'Signature\' no espaço para nome \'http://www.w3.org/2000/09/xmldsig#\'.\r\n---\r\n',9),
 (45,27,'2009-03-04 16:44:17','',6),
 (46,10533,'2009-03-05 10:03:19','Tentativa n. 1',1),
 (47,10533,'2009-03-05 10:03:19','',4),
 (48,10533,'2009-03-05 10:05:06','Tentativa n. 2',1),
 (49,10533,'2009-03-05 10:05:21','',4),
 (50,10533,'2009-03-05 10:05:50','Tentativa n. 3',1),
 (51,10533,'2009-03-05 10:10:58','Tentativa n. 3',1),
 (52,10533,'2009-03-05 10:17:44','',4),
 (53,10533,'2009-03-05 10:18:56','Tentativa n. 4',1),
 (54,10533,'2009-03-05 10:19:01','Tentativa n. 4',1),
 (55,10533,'2009-03-05 10:19:08','Tentativa n. 4',1),
 (56,10533,'2009-03-05 10:19:15','',3),
 (57,10533,'2009-03-05 10:19:17','',3),
 (58,10533,'2009-03-05 10:19:33','Tentativa n. 4',1),
 (59,10533,'2009-03-05 10:19:47','',3),
 (60,10533,'2009-03-05 10:20:01','Tentativa n. 4',1),
 (61,10533,'2009-03-05 10:21:46','Tentativa n. 4',1),
 (62,10533,'2009-03-05 10:26:43','',3),
 (63,10533,'2009-03-05 10:32:19','Tentativa n. 4',1),
 (64,10533,'2009-03-05 10:32:29','',3),
 (65,10533,'2009-03-05 10:36:26','Tentativa n. 4',1),
 (66,10533,'2009-03-05 10:36:29','',3),
 (67,10533,'2009-03-05 10:39:02','Tentativa n. 4',1),
 (68,10533,'2009-03-05 10:39:04','',3),
 (69,10533,'2009-03-05 11:41:13','Tentativa n. 5',1),
 (70,10533,'2009-03-05 11:41:14','',4),
 (71,10533,'2009-03-05 11:41:47','Tentativa n. 6',1),
 (72,10533,'2009-03-05 11:42:01','',4),
 (73,10533,'2009-03-05 11:42:37','Tentativa n. 7',1),
 (74,10533,'2009-03-05 11:44:25','Tentativa n. 7',1),
 (75,10533,'2009-03-05 11:45:36','',4),
 (76,10533,'2009-03-05 11:46:50','Tentativa n. 8',1),
 (77,10533,'2009-03-05 11:47:10','',3),
 (78,10533,'2009-03-05 11:49:41','Tentativa n. 9',1),
 (79,10533,'2009-03-05 11:49:42','',3),
 (80,10533,'2009-03-05 11:50:38','',7),
 (81,10533,'2009-03-05 11:55:26','Tentativa n. 10',1),
 (82,10533,'2009-03-05 11:55:27','',3),
 (83,10533,'2009-03-05 11:57:22','Erro(linha 3 posição 25): O atributo \'Id\' é inválido - O valor \'\' é inválido dependendo do tipo de dados \'http://www.w3.org/2001/XMLSchema:ID\' - A seqüência vazia \'\' não é um nome válido.\r\n---\r\nErro(linha 18 posição 14): O elemento \'http://www.portalfiscal.inf.br/nfe:cDV\' é inválido - O valor \'\' é inválido dependendo do tipo de dados \'String\' - Falha na restrição Pattern.\r\n---\r\nErro(linha 134 posição 3): O elemento \'NFe\' no espaço para nome \'http://www.portalfiscal.inf.br/nfe\' apresenta conteúdo incompleto. Lista de possíveis elementos esperados: \'Signature\' no espaço para nome \'http://www.w3.org/2000/09/xmldsig#\'.\r\n---\r\n',9),
 (84,10533,'2009-03-05 11:59:25','Tentativa n. 11',1),
 (85,10533,'2009-03-05 11:59:25','',3),
 (86,10533,'2009-03-05 12:04:48','Tentativa n. 12',1),
 (87,10533,'2009-03-05 12:04:48','',3),
 (88,10533,'2009-03-05 12:04:58','Erro(linha 3 posição 25): O atributo \'Id\' é inválido - O valor \'\' é inválido dependendo do tipo de dados \'http://www.w3.org/2001/XMLSchema:ID\' - A seqüência vazia \'\' não é um nome válido.\r\n---\r\nErro(linha 18 posição 14): O elemento \'http://www.portalfiscal.inf.br/nfe:cDV\' é inválido - O valor \'\' é inválido dependendo do tipo de dados \'String\' - Falha na restrição Pattern.\r\n---\r\nErro(linha 134 posição 3): O elemento \'NFe\' no espaço para nome \'http://www.portalfiscal.inf.br/nfe\' apresenta conteúdo incompleto. Lista de possíveis elementos esperados: \'Signature\' no espaço para nome \'http://www.w3.org/2000/09/xmldsig#\'.\r\n---\r\n',9),
 (89,10533,'2009-03-05 12:05:16','',6),
 (90,10534,'2009-03-05 12:06:21','Tentativa n. 1',1),
 (91,10534,'2009-03-05 12:06:21','',3),
 (92,10534,'2009-03-05 12:06:28','Erro(linha 3 posição 25): O atributo \'Id\' é inválido - O valor \'\' é inválido dependendo do tipo de dados \'http://www.w3.org/2001/XMLSchema:ID\' - A seqüência vazia \'\' não é um nome válido.\r\n---\r\nErro(linha 18 posição 14): O elemento \'http://www.portalfiscal.inf.br/nfe:cDV\' é inválido - O valor \'\' é inválido dependendo do tipo de dados \'String\' - Falha na restrição Pattern.\r\n---\r\nErro(linha 134 posição 3): O elemento \'NFe\' no espaço para nome \'http://www.portalfiscal.inf.br/nfe\' apresenta conteúdo incompleto. Lista de possíveis elementos esperados: \'Signature\' no espaço para nome \'http://www.w3.org/2000/09/xmldsig#\'.\r\n---\r\n',9),
 (93,10534,'2009-03-05 12:07:04','',6),
 (94,10535,'2009-03-05 12:07:04','Tentativa n. 1',1),
 (95,10535,'2009-03-05 12:07:04','',3),
 (96,10535,'2009-03-05 12:07:12','Erro(linha 3 posição 25): O atributo \'Id\' é inválido - O valor \'\' é inválido dependendo do tipo de dados \'http://www.w3.org/2001/XMLSchema:ID\' - A seqüência vazia \'\' não é um nome válido.\r\n---\r\nErro(linha 18 posição 14): O elemento \'http://www.portalfiscal.inf.br/nfe:cDV\' é inválido - O valor \'\' é inválido dependendo do tipo de dados \'String\' - Falha na restrição Pattern.\r\n---\r\nErro(linha 134 posição 3): O elemento \'NFe\' no espaço para nome \'http://www.portalfiscal.inf.br/nfe\' apresenta conteúdo incompleto. Lista de possíveis elementos esperados: \'Signature\' no espaço para nome \'http://www.w3.org/2000/09/xmldsig#\'.\r\n---\r\n',9),
 (97,10535,'2009-03-05 12:08:02','',6),
 (98,10536,'2009-03-05 12:08:11','Tentativa n. 1',1),
 (99,10536,'2009-03-05 12:08:11','',3),
 (100,10536,'2009-03-05 12:08:18','Erro(linha 3 posição 25): O atributo \'Id\' é inválido - O valor \'\' é inválido dependendo do tipo de dados \'http://www.w3.org/2001/XMLSchema:ID\' - A seqüência vazia \'\' não é um nome válido.\r\n---\r\nErro(linha 18 posição 14): O elemento \'http://www.portalfiscal.inf.br/nfe:cDV\' é inválido - O valor \'\' é inválido dependendo do tipo de dados \'String\' - Falha na restrição Pattern.\r\n---\r\nErro(linha 134 posição 3): O elemento \'NFe\' no espaço para nome \'http://www.portalfiscal.inf.br/nfe\' apresenta conteúdo incompleto. Lista de possíveis elementos esperados: \'Signature\' no espaço para nome \'http://www.w3.org/2000/09/xmldsig#\'.\r\n---\r\n',9),
 (101,10539,'2009-03-05 12:22:49','Tentativa n. 1',1),
 (102,10539,'2009-03-05 12:22:49','',3),
 (103,10539,'2009-03-05 12:26:06','Tentativa n. 2',1),
 (104,10539,'2009-03-05 12:26:06','',3),
 (105,10539,'2009-03-05 12:27:28','Tentativa n. 3',1),
 (106,10539,'2009-03-05 12:27:28','',3),
 (107,10539,'2009-03-05 12:29:09','Tentativa n. 4',1),
 (108,10539,'2009-03-05 12:29:10','',3),
 (109,10539,'2009-03-05 12:30:30','Tentativa n. 5',1),
 (110,10539,'2009-03-05 12:30:30','',3),
 (111,10539,'2009-03-05 12:34:15','',6);
/*!40000 ALTER TABLE `historico` ENABLE KEYS */;


--
-- Definition of table `notas`
--

DROP TABLE IF EXISTS `notas`;
CREATE TABLE `notas` (
  `NFe_ide_nNF` int(9) unsigned NOT NULL default '0',
  `pastaDeTrabalho` varchar(100) default NULL,
  `statusDaNota` tinyint(4) default NULL,
  `tentativasDeInclusao` int(10) unsigned default '1',
  `NFe_ide_dEmi` date default NULL,
  `NFe_infNFe_id` varchar(50) default NULL,
  `NFe_emit_CNPJ` varchar(14) default NULL,
  `NFe_emit_CPF` varchar(11) default NULL,
  `NFe_emit_xNome` varchar(60) default NULL,
  `NFe_dest_CNPJ` varchar(14) default NULL,
  `NFe_dest_CPF` varchar(11) default NULL,
  `NFe_dest_xNome` varchar(60) default NULL,
  `NFe_dest_UF` varchar(2) default NULL,
  `NFe_total_ICMSTot_vNF` decimal(15,2) default NULL,
  `retEnviNFe_infRec_nRec` varchar(15) default NULL,
  `retEnviNFe_cStat` varchar(3) default NULL,
  `retEnviNFe_xMotivo` varchar(255) default NULL,
  `protNfe_nProt` varchar(15) default NULL,
  `dataUltimaAtualizacao` timestamp NOT NULL default CURRENT_TIMESTAMP on update CURRENT_TIMESTAMP,
  `impressaoSolicitada` tinyint(1) default '0',
  `impressoEmContingencia` tinyint(1) default '0',
  PRIMARY KEY  (`NFe_ide_nNF`),
  UNIQUE KEY `NFe_ide_nNF` (`NFe_ide_nNF`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 ROW_FORMAT=DYNAMIC;

--
-- Dumping data for table `notas`
--

/*!40000 ALTER TABLE `notas` DISABLE KEYS */;
INSERT INTO `notas` (`NFe_ide_nNF`,`pastaDeTrabalho`,`statusDaNota`,`tentativasDeInclusao`,`NFe_ide_dEmi`,`NFe_infNFe_id`,`NFe_emit_CNPJ`,`NFe_emit_CPF`,`NFe_emit_xNome`,`NFe_dest_CNPJ`,`NFe_dest_CPF`,`NFe_dest_xNome`,`NFe_dest_UF`,`NFe_total_ICMSTot_vNF`,`retEnviNFe_infRec_nRec`,`retEnviNFe_cStat`,`retEnviNFe_xMotivo`,`protNfe_nProt`,`dataUltimaAtualizacao`,`impressaoSolicitada`,`impressoEmContingencia`) VALUES 
 (27,'C:\\Fisconet4\\process\\2009\\3\\4\\27\\',0,2,'0001-01-01',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'0.00',NULL,NULL,NULL,NULL,'2009-03-04 16:32:56',0,0),
 (10533,'C:\\Fisconet4\\process\\2009\\3\\5\\10533\\',3,12,'2009-03-05','','02549051000100',NULL,'ATADIESEL COMERCIO DE DIESEL E LUBRIFICANTES LTDA.','02549051000100','','ATADIESEL COM. DE DIESEL E LUBR. LTDA','SP','1130000.00',NULL,NULL,NULL,NULL,'2009-03-05 12:04:48',0,0),
 (10534,'C:\\Fisconet4\\process\\2009\\3\\5\\10534\\',3,1,'2009-03-05','','02549051000100',NULL,'ATADIESEL COMERCIO DE DIESEL E LUBRIFICANTES LTDA.','02549051000100','','ATADIESEL COM. DE DIESEL E LUBR. LTDA','SP','1130000.00',NULL,NULL,NULL,NULL,'2009-03-05 12:06:28',0,0),
 (10535,'C:\\Fisconet4\\process\\2009\\3\\5\\10535\\',3,1,'2009-03-05','','02549051000100',NULL,'ATADIESEL COMERCIO DE DIESEL E LUBRIFICANTES LTDA.','02549051000100','','ATADIESEL COM. DE DIESEL E LUBR. LTDA','SP','1130000.00',NULL,NULL,NULL,NULL,'2009-03-05 12:07:12',0,0),
 (10536,'C:\\Fisconet4\\process\\2009\\3\\5\\10536\\',3,1,'2009-03-05','','02549051000100',NULL,'ATADIESEL COMERCIO DE DIESEL E LUBRIFICANTES LTDA.','02549051000100','','ATADIESEL COM. DE DIESEL E LUBR. LTDA','SP','1130000.00',NULL,NULL,NULL,NULL,'2009-03-05 12:08:18',0,0),
 (10539,'C:\\Fisconet4\\process\\2009\\3\\5\\10539\\',0,5,'2009-03-05','NFe35090302549051000100550000000105330000105330','02549051000100',NULL,'ATADIESEL COMERCIO DE DIESEL E LUBRIFICANTES LTDA.','02549051000100','','ATADIESEL COM. DE DIESEL E LUBR. LTDA','SP','1130000.00',NULL,NULL,NULL,NULL,'2009-03-05 12:30:30',0,0);
/*!40000 ALTER TABLE `notas` ENABLE KEYS */;


--
-- Definition of table `tipodehistorico`
--

DROP TABLE IF EXISTS `tipodehistorico`;
CREATE TABLE `tipodehistorico` (
  `idTpHistorico` int(10) unsigned NOT NULL auto_increment,
  `tpHistorico` varchar(255) NOT NULL,
  PRIMARY KEY  (`idTpHistorico`)
) ENGINE=InnoDB AUTO_INCREMENT=17 DEFAULT CHARSET=utf8;

--
-- Dumping data for table `tipodehistorico`
--

/*!40000 ALTER TABLE `tipodehistorico` DISABLE KEYS */;
INSERT INTO `tipodehistorico` (`idTpHistorico`,`tpHistorico`) VALUES 
 (1,'Entrada da nota no sistema '),
 (2,'Erro: A Nota já foi autorizada e não pode ser re-processada'),
 (3,'Conversão de TXT para XML efetuada com sucesso'),
 (4,'Erro: Não foi possível a conversão do TXT para o XML. Entrar em contato com o Help Desk.'),
 (5,'Erro: Falha na conversão do XML para o layout do schema atual'),
 (6,'A nota foi assinada digitalmente'),
 (7,'Erro: Não foi possível assinar digitalmente a Nota. Possíveis erros: Certificado expirado ou não localizado.'),
 (8,'Validação Estrutural da Nota, baseada no Layout '),
 (9,'Erro de validação. Corrija os dados e processe novamente, caso ainda restem dúvidas, solicitar o Help Desk'),
 (10,'Montagem de Lote'),
 (11,'Envio da NFe para o Sefaz'),
 (12,'Erro: A nfe não pode ser enviada, possíveis erros: Sefaz inativa ou falha na comunicação.'),
 (13,'Retorno da nfe da sefaz, com o seguinte status: '),
 (14,'Entrada da nfe em contingência'),
 (15,'Impressão do Danfe em contingência solicitado pelo usuário'),
 (16,'Envio dos dados da nfe para o destinatário');
/*!40000 ALTER TABLE `tipodehistorico` ENABLE KEYS */;


--
-- Definition of table `usuarios`
--

DROP TABLE IF EXISTS `usuarios`;
CREATE TABLE `usuarios` (
  `idUsuario` int(10) unsigned NOT NULL auto_increment,
  `email` varchar(40) NOT NULL,
  `senha` varchar(20) NOT NULL,
  `tipo` tinyint(4) NOT NULL,
  PRIMARY KEY  USING BTREE (`idUsuario`),
  KEY `AI_id_usuario` USING BTREE (`idUsuario`)
) ENGINE=MyISAM AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

--
-- Dumping data for table `usuarios`
--

/*!40000 ALTER TABLE `usuarios` DISABLE KEYS */;
INSERT INTO `usuarios` (`idUsuario`,`email`,`senha`,`tipo`) VALUES 
 (1,'adm@megaideas.net','12345',1);
/*!40000 ALTER TABLE `usuarios` ENABLE KEYS */;




/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
