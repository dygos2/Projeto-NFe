<xsl:stylesheet xmlns="http://www.portalfiscal.inf.br/nfe" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">
  <xsl:output encoding="UTF-8" indent="yes" method="xml"></xsl:output>
  <xsl:template match="/">
    <NFe>
      <infNFe versao="3.10">
        <xsl:attribute name="Id">
          <xsl:value-of select="NFe/infNFe/@Id"/>
        </xsl:attribute>
        <ide>
          <cUF>
            <xsl:value-of select="NFe/infNFe/ide/cUF"/>
          </cUF>
          <cNF>
            <xsl:value-of select="format-number(NFe/infNFe/ide/cNF,'00000000')"/>
          </cNF>
          <natOp>
            <xsl:value-of select="NFe/infNFe/ide/natOp"/>
          </natOp>
          <indPag>
            <xsl:value-of select="NFe/infNFe/ide/indPag"/>
          </indPag>
          <mod>
            <xsl:value-of select="NFe/infNFe/ide/mod"/>
          </mod>
          <serie>
            <xsl:value-of select="number(NFe/infNFe/ide/serie)"/>
          </serie>
          <nNF>
            <xsl:value-of select="number(NFe/infNFe/ide/nNF)"/>
          </nNF>
          <dhEmi>
            <xsl:value-of select="NFe/infNFe/ide/dhEmi"/>
          </dhEmi>
          <xsl:if test="NFe/infNFe/ide/dhSaiEnt != '' ">
            <dhSaiEnt>
              <xsl:value-of select="NFe/infNFe/ide/dhSaiEnt"/>
            </dhSaiEnt>
          </xsl:if>
          <tpNF>
            <xsl:value-of select="number(NFe/infNFe/ide/tpNF)"/>
          </tpNF>
          <idDest>
            <xsl:value-of select="number(NFe/infNFe/ide/idDest)"/>
          </idDest>
          <cMunFG>
            <xsl:value-of select="NFe/infNFe/ide/cMunFG"/>
          </cMunFG>
          <xsl:for-each select="NFe/infNFe/ide/NFref">
            <xsl:if test="refNFe != ''">
              <NFref>
                <refNFe>
                  <xsl:value-of select="refNFe" />
                </refNFe>
              </NFref>
            </xsl:if>
            <xsl:if test="refNF/nNF != ''">
              <NFref>
                <refNF>
                  <cUF>
                    <xsl:value-of select="refNF/cUF" />
                  </cUF>
                  <AAMM>
                    <xsl:value-of select="refNF/AAMM" />
                  </AAMM>
                  <CNPJ>
                    <xsl:value-of select="refNF/CNPJ" />
                  </CNPJ>
                  <mod>
                    <xsl:value-of select="refNF/mod" />
                  </mod>
                  <serie>
                    <xsl:value-of select="refNF/serie" />
                  </serie>
                  <nNF>
                    <xsl:value-of select="refNF/nNF" />
                  </nNF>
                </refNF>
              </NFref>
            </xsl:if>
            <xsl:if test="refNFP/cUF != ''">
              <NFref>
                <refNFP>
                  <cUF>
                    <xsl:value-of select="refNFP/cUF" />
                  </cUF>
                  <AAMM>
                    <xsl:value-of select="refNFP/AAMM" />
                  </AAMM>
                  <xsl:if test="refNFP/CNPJ != ''">
                    <CNPJ>
                      <xsl:value-of select="refNFP/CNPJ" />
                    </CNPJ>
                  </xsl:if>
                  <xsl:if test="refNFP/CPF != ''">
                    <CNPJ>
                      <xsl:value-of select="refNFP/CPF" />
                    </CNPJ>
                  </xsl:if>
                  <IE>
                    <xsl:value-of select="refNFP/IE" />
                  </IE>
                  <mod>
                    <xsl:value-of select="refNFP/mod" />
                  </mod>
                  <serie>
                    <xsl:value-of select="refNFP/serie" />
                  </serie>
                  <nNF>
                    <xsl:value-of select="refNFP/nNF" />
                  </nNF>
                </refNFP>
              </NFref>
            </xsl:if>
            <xsl:if test="refCTe != ''">
              <refCTe>
		<xsl:value-of select="refCTe" />
              </refCTe>
            </xsl:if>
            <xsl:if test="refECF/nECF != ''">
              <mod>
                <xsl:value-of select="refECF/mod" />
              </mod>
              <nECF>
                <xsl:value-of select="refECF/nECF" />
              </nECF>
              <nCOO>
                <xsl:value-of select="refECF/nCOO" />
              </nCOO>
            </xsl:if>
          </xsl:for-each>
          <tpImp>
            <xsl:value-of select="number(NFe/infNFe/ide/tpImp)"/>
          </tpImp>
          <tpEmis>
            <xsl:value-of select="NFe/infNFe/ide/tpEmis"/>
          </tpEmis>
          <cDV>
            <xsl:value-of select="NFe/infNFe/ide/cDV"/>
          </cDV>
          <tpAmb>
            <xsl:value-of select="NFe/infNFe/ide/tpAmb"/>
          </tpAmb>
          <finNFe>
            <xsl:value-of select="NFe/infNFe/ide/finNFe"/>
          </finNFe>
          <indFinal>
            <xsl:value-of select="NFe/infNFe/ide/indFinal"/>
          </indFinal>
          <indPres>
            <xsl:value-of select="NFe/infNFe/ide/indPres"/>
          </indPres>
          <procEmi>0</procEmi>
          <verProc>2.00</verProc>
        </ide>
        <emit>
          <CNPJ>
            <xsl:value-of select="NFe/infNFe/emit/CNPJ"/>
          </CNPJ>
          <xNome>
            <xsl:value-of select="NFe/infNFe/emit/xNome"/>
          </xNome>
          <enderEmit>
            <xLgr>
              <xsl:value-of select="NFe/infNFe/emit/enderEmit/xLgr"/>
            </xLgr>
            <nro>
              <xsl:value-of select="NFe/infNFe/emit/enderEmit/nro"/>
            </nro>
            <xsl:if test="NFe/infNFe/emit/enderEmit/xCpl != ''">
              <xCpl>
                <xsl:value-of select="NFe/infNFe/emit/enderEmit/xCpl"/>
              </xCpl>
            </xsl:if>
            <xsl:if test="NFe/infNFe/emit/enderEmit/xBairro != ''">
              <xBairro>
                <xsl:value-of select="NFe/infNFe/emit/enderEmit/xBairro"/>
              </xBairro>
            </xsl:if>
            <xsl:if test="NFe/infNFe/emit/enderEmit/xBairro = ''">
              <xBairro>--</xBairro>
            </xsl:if>
            <cMun>
              <xsl:value-of select="NFe/infNFe/emit/enderEmit/cMun"/>
            </cMun>
            <xMun>
              <xsl:value-of select="NFe/infNFe/emit/enderEmit/xMun"/>
            </xMun>
            <UF>
              <xsl:value-of select="NFe/infNFe/emit/enderEmit/UF"/>
            </UF>
            <CEP>
              <xsl:value-of select="NFe/infNFe/emit/enderEmit/CEP"/>
            </CEP>
          <xsl:if test="NFe/infNFe/emit/enderEmit/fone != ''">
              <fone>
                <xsl:value-of select="NFe/infNFe/emit/enderEmit/fone"/>
              </fone>
	  </xsl:if>
          </enderEmit>

          <IE>
            <xsl:value-of select="NFe/infNFe/emit/IE"/>
          </IE>
          <xsl:if test="NFe/infNFe/emit/IEST != ''">
            <IEST>
              <xsl:value-of select="NFe/infNFe/emit/IEST"/>
            </IEST>
          </xsl:if>

          <CRT>
            <xsl:value-of select="NFe/infNFe/emit/CRT"/>
          </CRT>

          <xsl:if test="NFe/infNFe/emit/IM != ''">
            <IM>
              <xsl:value-of select="NFe/infNFe/emit/IM"/>
            </IM>
          </xsl:if>
        </emit>
        <dest>
          <xsl:if test="NFe/infNFe/dest/CNPJ != '' and NFe/infNFe/dest/CNPJ != '99999999999999'">
            <CNPJ>
              <xsl:value-of select="NFe/infNFe/dest/CNPJ"/>
            </CNPJ>
          </xsl:if>
          <xsl:if test="NFe/infNFe/dest/CPF != '' and  NFe/infNFe/dest/CPF != 0">
            <CPF>
              <xsl:value-of select="translate(NFe/infNFe/dest/CPF,'-','')"/>
            </CPF>
          </xsl:if>
          <xsl:if test="NFe/infNFe/dest/CPF = '' and NFe/infNFe/dest/CNPJ = '' ">
            <CNPJ></CNPJ>
          </xsl:if>
          <xsl:if test="NFe/infNFe/dest/CNPJ = '99999999999999' ">
            <CNPJ></CNPJ>
          </xsl:if>
          <xsl:if test="NFe/infNFe/dest/idEstrangeiro != ''">
            <idEstrangeiro>
              <xsl:value-of select="NFe/infNFe/dest/idEstrangeiro"/>
            </idEstrangeiro>
          </xsl:if>
          <xNome>
            <xsl:value-of select="NFe/infNFe/dest/xNome"></xsl:value-of>
          </xNome>
          <enderDest>
            <xLgr>
              <xsl:value-of select="NFe/infNFe/dest/enderDest/xLgr"/>
            </xLgr>
            <nro>
              <xsl:value-of select="NFe/infNFe/dest/enderDest/nro"/>
            </nro>
            <xsl:if test="NFe/infNFe/dest/enderDest/xCpl != ''">
              <xCpl>
                <xsl:value-of select="NFe/infNFe/dest/enderDest/xCpl"/>
              </xCpl>
            </xsl:if>
            <xsl:if test="NFe/infNFe/dest/enderDest/xBairro != ''">
              <xBairro>
                <xsl:value-of select="NFe/infNFe/dest/enderDest/xBairro"/>
              </xBairro>
            </xsl:if>
            <xsl:if test="NFe/infNFe/dest/enderDest/xBairro = '' ">
              <xBairro>-</xBairro>
            </xsl:if>
            <cMun>
              <xsl:value-of select="NFe/infNFe/dest/enderDest/cMun"/>
            </cMun>
            <xMun>
              <xsl:value-of select="NFe/infNFe/dest/enderDest/xMun"/>
            </xMun>
            <UF>
              <xsl:value-of select="NFe/infNFe/dest/enderDest/UF"/>
            </UF>
            <xsl:if test="NFe/infNFe/dest/enderDest/CEP != '' and NFe/infNFe/dest/enderDest/CEP != 0 ">
              <CEP>
                <xsl:value-of select="NFe/infNFe/dest/enderDest/CEP"/>
              </CEP>
            </xsl:if>
            <xsl:if test="NFe/infNFe/dest/enderDest/cPais != ''">
            <cPais>
              <xsl:value-of select="NFe/infNFe/dest/enderDest/cPais"/>
            </cPais>
            </xsl:if>
            <xsl:if test="NFe/infNFe/dest/enderDest/xPais != ''">
            <xPais>
              <xsl:value-of select="NFe/infNFe/dest/enderDest/xPais"/>
            </xPais>
            </xsl:if>
          <xsl:if test="NFe/infNFe/dest/fone != ''">
              <fone>
                <xsl:value-of select="NFe/infNFe/dest/fone"/>
              </fone>
	  </xsl:if>
          </enderDest>
          <indIEDest>
            <xsl:value-of select="NFe/infNFe/dest/indIEDest"/>
          </indIEDest>
<xsl:if test="NFe/infNFe/dest/IE != '' and NFe/infNFe/dest/IE != '-'">
          <IE>
            <xsl:value-of select="NFe/infNFe/dest/IE"/>
          </IE>
</xsl:if>
            <xsl:if test="NFe/infNFe/dest/ISUF != ''">
	          <ISUF>
	            <xsl:value-of select="NFe/infNFe/dest/ISUF"/>
	          </ISUF>
            </xsl:if>
            <xsl:if test="NFe/infNFe/dest/email != ''">
	          <email>
	            <xsl:value-of select="NFe/infNFe/dest/email"/>
	          </email>
            </xsl:if>
        </dest>
        <xsl:if test="NFe/infNFe/retirada/CNPJ != '' or NFe/infNFe/retirada/CPF != ''">
          <retirada>
            <xsl:if test="NFe/infNFe/retirada/CNPJ != '' and NFe/infNFe/retirada/CNPJ != 0">
	            <CNPJ>
	              <xsl:value-of select="NFe/infNFe/retirada/CNPJ"/>
	            </CNPJ>
            </xsl:if>
            <xsl:if test="NFe/infNFe/retirada/CPF != '' and NFe/infNFe/retirada/CPF != 0">
              <CPF>
                <xsl:value-of select="translate(NFe/infNFe/retirada/CPF,'-','')"/>
              </CPF>
            </xsl:if>
            <xLgr>
              <xsl:value-of select="NFe/infNFe/retirada/xLgr"/>
            </xLgr>
            <nro>
              <xsl:value-of select="NFe/infNFe/retirada/nro"/>
            </nro>
            <xsl:if test="NFe/infNFe/retirada/xCpl != ''">
              <xCpl>
                <xsl:value-of select="NFe/infNFe/retirada/xCpl"/>
              </xCpl>
            </xsl:if>
            <xsl:if test="NFe/infNFe/retirada/xBairro != ''">
              <xBairro>
                <xsl:value-of select="NFe/infNFe/retirada/xBairro"/>
              </xBairro>
            </xsl:if>
            <xsl:if test="NFe/infNFe/retirada/xBairro = ''">
              <xBairro>-</xBairro>
            </xsl:if>
            <cMun>
              <xsl:value-of select="NFe/infNFe/retirada/cMun"/>
            </cMun>
            <xMun>
              <xsl:value-of select="NFe/infNFe/retirada/xMun"/>
            </xMun>
            <UF>
              <xsl:value-of select="NFe/infNFe/retirada/UF"/>
            </UF>
          </retirada>
        </xsl:if>
        <xsl:if test="(NFe/infNFe/entrega/CPF != '' or NFe/infNFe/entrega/CNPJ != '') and NFe/infNFe/entrega/CNPJ != '00000000000000'">
          <entrega>
            <xsl:if test="NFe/infNFe/entrega/CNPJ != '' and NFe/infNFe/entrega/CNPJ != 0">
	            <CNPJ>
	              <xsl:value-of select="NFe/infNFe/entrega/CNPJ"/>
	            </CNPJ>
            </xsl:if>
            <xsl:if test="NFe/infNFe/entrega/CPF != '' and NFe/infNFe/entrega/CPF != 0">
              <CPF>
                <xsl:value-of select="translate(NFe/infNFe/entrega/CPF,'-','')"/>
              </CPF>
            </xsl:if>
            <xLgr>
              <xsl:value-of select="NFe/infNFe/entrega/xLgr"/>
            </xLgr>
            <nro>
              <xsl:value-of select="NFe/infNFe/entrega/nro"/>
            </nro>
            <xsl:if test="NFe/infNFe/entrega/xCpl != ''">
              <xCpl>
                <xsl:value-of select="NFe/infNFe/entrega/xCpl"/>
              </xCpl>
            </xsl:if>
            <xsl:if test="NFe/infNFe/entrega/xBairro != ''">
              <xBairro>
                <xsl:value-of select="NFe/infNFe/entrega/xBairro"/>
              </xBairro>
            </xsl:if>
            <xsl:if test="NFe/infNFe/entrega/xBairro = ''">
              <xBairro>-</xBairro>
            </xsl:if>
            <cMun>
              <xsl:value-of select="NFe/infNFe/entrega/cMun"/>
            </cMun>
            <xMun>
              <xsl:value-of select="NFe/infNFe/entrega/xMun"/>
            </xMun>
            <UF>
              <xsl:value-of select="NFe/infNFe/entrega/UF"/>
            </UF>
          </entrega>
        </xsl:if>

        <!-- Produtos -->
        <xsl:for-each select="NFe/infNFe/det">
          <det>
            <xsl:attribute name="nItem">
              <xsl:value-of select="@nItem"/>
            </xsl:attribute>

            <prod>
              <cProd>
                <xsl:value-of select="prod/cProd"/>
              </cProd>
              <!-- inserido por obrigatoriedade -->
              <cEAN>
                <xsl:value-of select="prod/cEAN" />
              </cEAN>
              <xProd>
                <xsl:value-of select="prod/xProd"/>
              </xProd>
              <NCM>
                <xsl:value-of select="prod/NCM"/>
              </NCM>
              <xsl:if test="prod/NVE != ''">
                <NVE>
                  <xsl:value-of select="prod/NVE" />
                </NVE>
              </xsl:if>
              <xsl:if test="prod/EXTIPI != ''">
                <EXTIPI>
                  <xsl:value-of select="prod/EXTIPI" />
                </EXTIPI>
              </xsl:if>
              <CFOP>
                <xsl:value-of select="prod/CFOP"/>
              </CFOP>
              <uCom>
                <xsl:value-of select="prod/uCom"/>
              </uCom>
              <qCom>
                <xsl:value-of select="translate(format-number(prod/qCom,'#0.0000'),',','.')"/>
              </qCom>
              <xsl:if test="not(prod/vUnCom)">
                <vUnCom>
                  <xsl:value-of select="translate(format-number(prod/vProd div prod/qCom,'#0.000000'),',','.')"/>
                </vUnCom>
              </xsl:if>
              <xsl:if test="prod/vUnCom">
                <vUnCom>
                  <xsl:value-of select="format-number(prod/vUnCom,'#0.000000')"/>
                </vUnCom>
              </xsl:if>
              <vProd>
                <xsl:value-of select="format-number(number(prod/vProd),'#0.00')"/>
              </vProd>
              <!-- inserido por obrigatoriedade -->
              <cEANTrib>
	      <xsl:if test="number(prod/cEANTrib) > 0">	
                <xsl:value-of select="prod/cEANTrib" />
	      </xsl:if>
              </cEANTrib>

              <xsl:if test="prod/uTrib != ''">
                <uTrib>
                  <xsl:value-of select="prod/uTrib"/>
                </uTrib>
              </xsl:if>
              <xsl:if test="prod/uTrib = ''">
                <uTrib>-</uTrib>
              </xsl:if>

              <xsl:if test="prod/qTrib != ''">
                <qTrib>
                  <xsl:value-of select="translate(format-number(prod/qTrib,'#0.0000'),',','.')"/>
                </qTrib>
              </xsl:if>
              <xsl:if test="prod/qTrib = ''">
                <qTrib>0</qTrib>
              </xsl:if>

              <vUnTrib>
                <xsl:value-of select="translate(format-number(prod/vUnTrib,'#0.000000'),',','.')"/>
              </vUnTrib>

              <xsl:if test="prod/vFrete != 0 and prod/vFrete != '' ">
                <vFrete>
		  <xsl:value-of select="translate(format-number(prod/vFrete,'#0.00'),',','.')"/>
                </vFrete>
              </xsl:if>
              <xsl:if test="prod/vSeg != 0 and prod/vSeg != '' ">
                <vSeg>
                  <xsl:value-of select="prod/vSeg"/>
                </vSeg>
              </xsl:if>
              <xsl:if test="prod/vDesc != 0 and prod/vDesc != '' ">
                <vDesc>
                  <xsl:value-of select="prod/vDesc"/>
                </vDesc>
              </xsl:if>
              <xsl:if test="prod/vOutro != '' and prod/vOutro != 0">
                <vOutro>
                  <xsl:value-of select="prod/vOutro"/>
                </vOutro>
              </xsl:if>

              <indTot>
                <xsl:value-of select="prod/indTot"/>
              </indTot>

              <xsl:for-each select="prod/DI">

                <xsl:if test="nDI != 0 and nDI != '' ">
                  <DI>
                    <nDI>
                      <xsl:value-of select="nDI"/>
                    </nDI>
                    <dDI>
                      <xsl:value-of select="dDI"/>
                    </dDI>
                    <xLocDesemb>
                      <xsl:value-of select="xLocDesemb"/>
                    </xLocDesemb>
                    <UFDesemb>
                      <xsl:value-of select="UFDesemb"/>
                    </UFDesemb>
                    <dDesemb>
                      <xsl:value-of select="dDesemb"/>
                    </dDesemb>
                    <cExportador>
                      <xsl:value-of select="cExportador"/>
                    </cExportador>

                    <xsl:for-each select="adi">
		    <xsl:if test="nAdicao != 0 and nAdicao != '' ">
                      <adi>
                        <nAdicao>
                          <xsl:value-of select="nAdicao"/>
                        </nAdicao>
                        <nSeqAdic>
                          <xsl:value-of select="nSeqAdic"/>
                        </nSeqAdic>
                        <cFabricante>
                          <xsl:value-of select="cFabricante"/>
                        </cFabricante>

                        <xsl:if test="vDescDI != ''">
                          <vDescDI>
                            <xsl:value-of select="vDescDI" />
                          </vDescDI>
                        </xsl:if>
                       </adi>
                      </xsl:if>
                    </xsl:for-each>
                  </DI>
                </xsl:if>
              </xsl:for-each>

              <xsl:if test="prod/xPed != ''">
                <xPed>
                  <xsl:value-of select="prod/xPed" />
                </xPed>
              </xsl:if>

              <xsl:if test="prod/nItemPed != ''">
                <nItemPed>
                  <xsl:value-of select="prod/nItemPed" />
                </nItemPed>
              </xsl:if>

              <!-- veicProd -->
              <xsl:if test="prod/veicProd">
                <veicProd>
                  <tpOp>
                    <xsl:value-of select="prod/veicProd/tpOp" />
                  </tpOp>
                  <chassi>
                    <xsl:value-of select="prod/veicProd/chassi" />
                  </chassi>
                  <cCor>
                    <xsl:value-of select="prod/veicProd/cCor" />
                  </cCor>
                  <xCor>
                    <xsl:value-of select="prod/veicProd/xCor" />
                  </xCor>
                  <pot>
                    <xsl:value-of select="prod/veicProd/pot" />
                  </pot>
                  <cilin>
                    <xsl:value-of select="prod/veicProd/cilin" />
                  </cilin>
                  <pesoL>
                    <xsl:value-of select="prod/veicProd/pesoL" />
                  </pesoL>
                  <pesoB>
                    <xsl:value-of select="prod/veicProd/pesoB" />
                  </pesoB>
                  <nSerie>
                    <xsl:value-of select="prod/veicProd/nSerie" />
                  </nSerie>
                  <tpComb>
                    <xsl:value-of select="prod/veicProd/tpComb" />
                  </tpComb>
                  <nMotor>
                    <xsl:value-of select="prod/veicProd/nMotor" />
                  </nMotor>
                  <CMT>
                    <xsl:value-of select="prod/veicProd/CMT" />
                  </CMT>
                  <dist>
                    <xsl:value-of select="prod/veicProd/dist" />
                  </dist>
                  <anoMod>
                    <xsl:value-of select="prod/veicProd/anoMod" />
                  </anoMod>
                  <anoFab>
                    <xsl:value-of select="prod/veicProd/anoFab" />
                  </anoFab>
                  <tpPint>
                    <xsl:value-of select="prod/veicProd/tpPint" />
                  </tpPint>
                  <tpVeic>
                    <xsl:value-of select="prod/veicProd/tpVeic" />
                  </tpVeic>
                  <espVeic>
                    <xsl:value-of select="prod/veicProd/espVeic" />
                  </espVeic>
                  <VIN>
                    <xsl:value-of select="prod/veicProd/VIN" />
                  </VIN>
                  <condVeic>
                    <xsl:value-of select="prod/veicProd/condVeic" />
                  </condVeic>
                  <cMod>
                    <xsl:value-of select="prod/veicProd/cMod" />
                  </cMod>
                  <cCorDENATRAN>
                    <xsl:value-of select="prod/veicProd/cCorDENATRAN" />
                  </cCorDENATRAN>
                  <lota>
                    <xsl:value-of select="prod/veicProd/lota" />
                  </lota>
                  <tpRest>
                    <xsl:value-of select="prod/veicProd/tpRest" />
                  </tpRest>
                </veicProd>
              </xsl:if>
              <!-- Fim veicProd-->

              <!-- med -->
              <xsl:if test="prod/med">
                <xsl:for-each select="prod/med">
                  <med>
                    <nLote>
                      <xsl:value-of select="prod/med/nLote" />
                    </nLote>
                    <qLote>
                      <xsl:value-of select="prod/med/qLote" />
                    </qLote>
                    <dFab>
                      <xsl:value-of select="prod/med/dFab" />
                    </dFab>
                    <dVal>
                      <xsl:value-of select="prod/med/dVal" />
                    </dVal>
                    <vPMC>
                      <xsl:value-of select="prod/med/vPMC" />
                    </vPMC>
                  </med>
                </xsl:for-each>
              </xsl:if>
              <!-- Fim med -->

              <!-- arma -->
              <xsl:for-each select="prod/med">
                <tpArma>
                  <xsl:value-of select="prod/arma/tpArma" />
                </tpArma>
                <nSerie>
                  <xsl:value-of select="prod/arma/nSerie" />
                </nSerie>
                <nCano>
                  <xsl:value-of select="prod/arma/nCano" />
                </nCano>
                <descr>
                  <xsl:value-of select="prod/arma/descr" />
                </descr>
              </xsl:for-each>
              <!-- Fim arma -->

              <!-- comb -->
              <xsl:if test="prod/comb">
                <comb>
                  <cProdANP>
                    <xsl:value-of select="prod/comb/cProdANP" />
                  </cProdANP>
                  <xsl:if test="prod/comb/CODIF != ''">
                    <CODIF>
                      <xsl:value-of select="prod/comb/CODIF" />
                    </CODIF>
                  </xsl:if>
                  <xsl:if test="prod/comb/qTemp != ''">
                    <qTemp>
                      <xsl:value-of select="prod/comb/qTemp" />
                    </qTemp>
                  </xsl:if>
                  <UFCons>
                    <xsl:value-of select="prod/comb/UFCons" />
                  </UFCons>
                  <CIDE>
                    <qBCProd>
                      <xsl:value-of select="prod/comb/CIDE/qBCprod"/>
                    </qBCProd>
                    <vAliqProd>
                      <xsl:value-of select="prod/comb/CIDE/vAliqProd"/>
                    </vAliqProd>
                    <vCIDE>
                      <xsl:value-of select="prod/comb/CIDE/vCIDE"/>
                    </vCIDE>
                  </CIDE>
                </comb>
              </xsl:if>
              <!-- Fim comb-->

            </prod>

            <imposto>
              <xsl:choose>
                <xsl:when test="imposto/ISSQN/vISSQN != '' and number(imposto/ISSQN/vISSQN) != 0 ">
                  <ISSQN>
                    <vBC>
                      <xsl:value-of select="imposto/ISSQN/vBC"/>
                    </vBC>
                    <vAliq>
                      <xsl:value-of select="imposto/ISSQN/vAliq"/>
                    </vAliq>
                    <vISSQN>
                      <xsl:value-of select="imposto/ISSQN/vISSQN"/>
                    </vISSQN>
                    <cMunFG>
                      <xsl:value-of select="imposto/ISSQN/cMunFG"/>
                    </cMunFG>
                    <cListServ>
                      <xsl:value-of select="imposto/ISSQN/cListServ"/>
                    </cListServ>
                    <cSitTrib>
                      <xsl:value-of select="imposto/ISSQN/cSitTrib"/>
                    </cSitTrib>
                  </ISSQN>
                </xsl:when>
                <xsl:otherwise>
                  <ICMS>
                    <!-- Início ICMS00 -->
                    <xsl:if test="number(imposto/ICMS/ICMS99/CST) = 0">
                      <ICMS00>
                        <orig>
                          <xsl:value-of select="imposto/ICMS/ICMS99/orig"/>
                        </orig>
                        <CST>
                          <xsl:value-of select="format-number(imposto/ICMS/ICMS99/CST,'00')"/>
                        </CST>
                        <modBC>
                          <xsl:value-of select="imposto/ICMS/ICMS99/modBC"/>
                        </modBC>
                        <vBC>
                          <xsl:value-of select="format-number(imposto/ICMS/ICMS99/vBC, '#0.00') "/>
                        </vBC>
                        <pICMS>
                          <xsl:value-of select="imposto/ICMS/ICMS99/pICMS"/>
                        </pICMS>
                        <vICMS>
                          <xsl:value-of select="format-number(imposto/ICMS/ICMS99/vICMS,'#0.00') "/>
                        </vICMS>
                      </ICMS00>
                    </xsl:if>
                    <!-- Fim ICMS00 -->
                    <!-- Início ICMS10 -->
                    <xsl:if test="number(imposto/ICMS/ICMS99/CST) = 10">
                      <ICMS10>
                        <orig>
                          <xsl:value-of select="imposto/ICMS/ICMS99/orig"/>
                        </orig>
                        <CST>
                          <xsl:value-of select="format-number(imposto/ICMS/ICMS99/CST,'00')"/>
                        </CST>
                        <modBC>
                          <xsl:value-of select="imposto/ICMS/ICMS99/modBC"/>
                        </modBC>
                        <vBC>
                          <xsl:value-of select="format-number(imposto/ICMS/ICMS99/vBC, '#0.00') "/>
                        </vBC>
                        <pICMS>
                          <xsl:value-of select="imposto/ICMS/ICMS99/pICMS"/>
                        </pICMS>
                        <vICMS>
                          <xsl:value-of select="format-number(imposto/ICMS/ICMS99/vICMS, '#0.00')"/>
                        </vICMS>
                        <modBCST>
                          <xsl:value-of select="imposto/ICMS/ICMS99/modBCST"/>
                        </modBCST>
                        <xsl:if test="imposto/ICMS/ICMS99/pMVAST != ''">
                          <pMVAST>
                            <xsl:value-of select="imposto/ICMS/ICMS99/pMVAST" />
                          </pMVAST>
                        </xsl:if>
                        <xsl:if test="imposto/ICMS/ICMS99/pRedBCST != '' and number(imposto/ICMS/ICMS99/pRedBCST) != 0">
                          <pRedBCST>
                            <xsl:value-of select="imposto/ICMS/ICMS99/pRedBCST" />
                          </pRedBCST>
                        </xsl:if>
                        <vBCST>
                          <xsl:value-of select="format-number(imposto/ICMS/ICMS99/vBCST, '#0.00')"/>
                        </vBCST>
                        <pICMSST>
                          <xsl:value-of select="imposto/ICMS/ICMS99/pICMSST"/>
                        </pICMSST>
                        <vICMSST>
                          <xsl:value-of select="format-number(imposto/ICMS/ICMS99/vICMSST, '#0.00')"/>
                        </vICMSST>
                      </ICMS10>
                    </xsl:if>
                    <!-- Fim ICMS10 -->
                    <!-- Início ICMS20 -->
                    <xsl:if test="number(imposto/ICMS/ICMS99/CST) = 20">
                      <ICMS20>
                        <orig>
                          <xsl:value-of select="imposto/ICMS/ICMS99/orig"/>
                        </orig>
                        <CST>
                          <xsl:value-of select="format-number(imposto/ICMS/ICMS99/CST,'00')"/>
                        </CST>
                        <modBC>
                          <xsl:value-of select="imposto/ICMS/ICMS99/modBC"/>
                        </modBC>
                        <pRedBC>
                          <xsl:value-of select="imposto/ICMS/ICMS99/pRedBC"/>
                        </pRedBC>
                        <vBC>
                          <xsl:value-of select="format-number(imposto/ICMS/ICMS99/vBC, '#0.00')"/>
                        </vBC>
                        <pICMS>
                          <xsl:value-of select="imposto/ICMS/ICMS99/pICMS"/>
                        </pICMS>
                        <vICMS>
                          <xsl:value-of select="format-number(imposto/ICMS/ICMS99/vICMS, '#0.00')"/>
                        </vICMS>
                        <xsl:if test="imposto/ICMS/ICMS99/motDesICMS != '' and imposto/ICMS/ICMS99/vICMSDeson != ''">
                          <motDesICMS>
                            <xsl:value-of select="imposto/ICMS/ICMS99/motDesICMS" />
                          </motDesICMS>
                          <vICMSDeson>
                            <xsl:value-of select="imposto/ICMS/ICMS99/vICMSDeson" />
                          </vICMSDeson>
                        </xsl:if>
                      </ICMS20>
                    </xsl:if>
                    <!-- Fim ICMS20 -->
                    <!-- Início ICMS30 -->
                    <xsl:if test="number(imposto/ICMS/ICMS99/CST) = 30">
                      <ICMS30>
                        <orig>
                          <xsl:value-of select="imposto/ICMS/ICMS99/orig"/>
                        </orig>
                        <CST>
                          <xsl:value-of select="format-number(imposto/ICMS/ICMS99/CST,'00')"/>
                        </CST>
                        <modBCST>
                          <xsl:value-of select="imposto/ICMS/ICMS99/modBCST"/>
                        </modBCST>
                        <xsl:if test="imposto/ICMS/ICMS99/pMVAST != ''">
                          <pMVAST>
                            <xsl:value-of select="imposto/ICMS/ICMS99/pMVAST" />
                          </pMVAST>
                        </xsl:if>
                        <xsl:if test="imposto/ICMS/ICMS99/pRedBCST != '' and number(imposto/ICMS/ICMS99/pRedBCST) != 0">
                          <pRedBCST>
                            <xsl:value-of select="imposto/ICMS/ICMS99/pRedBCST" />
                          </pRedBCST>
                        </xsl:if>
                        <vBCST>
                          <xsl:value-of select="format-number(imposto/ICMS/ICMS99/vBCST, '#0.00')"/>
                        </vBCST>
                        <pICMSST>
                          <xsl:value-of select="imposto/ICMS/ICMS99/pICMSST"/>
                        </pICMSST>
                        <vICMSST>
                          <xsl:value-of select="format-number(imposto/ICMS/ICMS99/vICMSST, '#0.00')"/>
                        </vICMSST>
                        <xsl:if test="imposto/ICMS/ICMS99/motDesICMS != '' and imposto/ICMS/ICMS99/vICMSDeson != ''">
                          <motDesICMS>
                            <xsl:value-of select="imposto/ICMS/ICMS99/motDesICMS" />
                          </motDesICMS>
                          <vICMSDeson>
                            <xsl:value-of select="imposto/ICMS/ICMS99/vICMSDeson" />
                          </vICMSDeson>
                        </xsl:if>
                      </ICMS30>
                    </xsl:if>
                    <!-- Fim ICMS30 -->
                    <!-- Início ICMS40 / CST 40, 41 e 50 -->
                    <xsl:if test="number(imposto/ICMS/ICMS99/CST) = 40 or number(imposto/ICMS/ICMS99/CST) = 41 or number(imposto/ICMS/ICMS99/CST) = 50">
                      <ICMS40>
                        <orig>
                          <xsl:value-of select="imposto/ICMS/ICMS99/orig"/>
                        </orig>
                        <CST>
                          <xsl:value-of select="format-number(imposto/ICMS/ICMS99/CST,'00')"/>
                        </CST>
                        <xsl:if test="imposto/ICMS/ICMS99/vICMS != '' and number(imposto/ICMS/ICMS99/vICMS) > 0">
                          <vICMS>
                            <xsl:value-of select="imposto/ICMS/ICMS99/vICMS" />
                          </vICMS>
                        </xsl:if>
                        <xsl:if test="imposto/ICMS/ICMS99/motDesICMS != '' and imposto/ICMS/ICMS99/vICMSDeson != ''">
                          <motDesICMS>
                            <xsl:value-of select="imposto/ICMS/ICMS99/motDesICMS" />
                          </motDesICMS>
                          <vICMSDeson>
                            <xsl:value-of select="imposto/ICMS/ICMS99/vICMSDeson" />
                          </vICMSDeson>
                        </xsl:if>
                      </ICMS40>
                    </xsl:if>
                    <!-- Fim ICMS40 / CST 40, 41 e 50 -->
                    <!-- Início ICMS51 -->
                    <xsl:if test="number(imposto/ICMS/ICMS99/CST) = 51">
                      <ICMS51>
                        <orig>
                          <xsl:value-of select="imposto/ICMS/ICMS99/orig"/>
                        </orig>
                        <CST>
                          <xsl:value-of select="format-number(imposto/ICMS/ICMS99/CST,'00')"/>
                        </CST>
                        <xsl:if test="imposto/ICMS/ICMS99/modBC != ''">
                          <modBC>
                            <xsl:value-of select="imposto/ICMS/ICMS99/modBC"/>
                          </modBC>
                        </xsl:if>
                        <xsl:if test="imposto/ICMS/ICMS99/pRedBC != ''">
                          <pRedBC>
                            <xsl:value-of select="imposto/ICMS/ICMS99/pRedBC"/>
                          </pRedBC>
                        </xsl:if>
                        <xsl:if test="imposto/ICMS/ICMS99/vBC != ''">
                          <vBC>
                            <xsl:value-of select="format-number(imposto/ICMS/ICMS99/vBC, '#0.00')"/>
                          </vBC>
                        </xsl:if>
                        <xsl:if test="imposto/ICMS/ICMS99/pICMS != ''">
                          <pICMS>
                            <xsl:value-of select="imposto/ICMS/ICMS99/pICMS"/>
                          </pICMS>
                        </xsl:if>
                        <xsl:if test="imposto/ICMS/ICMS99/vICMS != ''">
                          <vICMS>
                            <xsl:value-of select="format-number(imposto/ICMS/ICMS99/vICMS, '#0.00')"/>
                          </vICMS>
                        </xsl:if>
						
                        <xsl:if test="imposto/ICMS/ICMS99/vICMSOp != ''">
                          <vICMSOp>
                            <xsl:value-of select="format-number(imposto/ICMS/ICMS99/vICMSOp, '#0.00')"/>
                          </vICMSOp>
                        </xsl:if>
                        <xsl:if test="imposto/ICMS/ICMS99/pDif != ''">
                          <pDif>
                            <xsl:value-of select="format-number(imposto/ICMS/ICMS99/pDif, '#0.0000')"/>
                          </pDif>
                        </xsl:if>
                        <xsl:if test="imposto/ICMS/ICMS99/vICMSDif != ''">
                          <vICMSDif>
                            <xsl:value-of select="format-number(imposto/ICMS/ICMS99/vICMSDif, '#0.00')"/>
                          </vICMSDif>
                        </xsl:if>
                      </ICMS51>
                    </xsl:if>
                    <!-- Fim ICMS51 -->
                    <!-- Início ICMS60 -->
                    <xsl:if test="number(imposto/ICMS/ICMS99/CST) = 60">
                      <ICMS60>
                        <orig>
                          <xsl:value-of select="imposto/ICMS/ICMS99/orig"/>
                        </orig>
                        <CST>
                          <xsl:value-of select="format-number(imposto/ICMS/ICMS99/CST,'00')"/>
                        </CST>
                        <xsl:if test="imposto/ICMS/ICMS99/vBCSTRet != ''">
                          <vBCSTRet>
                            <xsl:value-of select="imposto/ICMS/ICMS99/vBCSTRet" />
                          </vBCSTRet>
                        </xsl:if>
                        <xsl:if test="imposto/ICMS/ICMS99/vICMSSTRet != ''">
                          <vICMSSTRet>
                            <xsl:value-of select="imposto/ICMS/ICMS99/vICMSSTRet" />
                          </vICMSSTRet>
                        </xsl:if>
                      </ICMS60>
                    </xsl:if>
                    <!-- Fim ICMS60 -->
                    <!-- Início ICMS70 -->
                    <xsl:if test="number(imposto/ICMS/ICMS99/CST) = 70">
                      <ICMS70>
                        <orig>
                          <xsl:value-of select="imposto/ICMS/ICMS99/orig"/>
                        </orig>
                        <CST>
                          <xsl:value-of select="format-number(imposto/ICMS/ICMS99/CST,'00')"/>
                        </CST>
                        <modBC>
                          <xsl:value-of select="imposto/ICMS/ICMS99/modBC" />
                        </modBC>
                        <pRedBC>
                          <xsl:value-of select="imposto/ICMS/ICMS99/pRedBC" />
                        </pRedBC>
                        <vBC>
                          <xsl:value-of select="format-number(imposto/ICMS/ICMS99/vBC, '#0.00')"/>
                        </vBC>
                        <pICMS>
                          <xsl:value-of select="format-number(imposto/ICMS/ICMS99/pICMS, '#0.00')"/>
                        </pICMS>
                        <vICMS>
                          <xsl:value-of select="format-number(imposto/ICMS/ICMS99/vICMS, '#0.00')"/>
                        </vICMS>
                        <modBCST>
                          <xsl:value-of select="imposto/ICMS/ICMS99/modBCST"/>
                        </modBCST>
                        <xsl:if test="imposto/ICMS/ICMS99/pMVAST != ''">
                          <pMVAST>
                            <xsl:value-of select="format-number(imposto/ICMS/ICMS99/pMVAST, '#0.00')" />
                          </pMVAST>
                        </xsl:if>
                        <xsl:if test="imposto/ICMS/ICMS99/pRedBCST != '' and number(imposto/ICMS/ICMS99/pRedBCST) != 0">
                          <pRedBCST>
                            <xsl:value-of select="format-number(imposto/ICMS/ICMS99/pRedBCST, '#0.00')" />
                          </pRedBCST>
                        </xsl:if>
                        <vBCST>
                          <xsl:value-of select="format-number(imposto/ICMS/ICMS99/vBCST, '#0.00')"/>
                        </vBCST>
                        <pICMSST>
                          <xsl:value-of select="format-number(imposto/ICMS/ICMS99/pICMSST, '#0.00')"/>
                        </pICMSST>
                        <vICMSST>
                          <xsl:value-of select="format-number(imposto/ICMS/ICMS99/vICMSST, '#0.00')"/>
                        </vICMSST>
                        <xsl:if test="imposto/ICMS/ICMS99/motDesICMS != '' and imposto/ICMS/ICMS99/vICMSDeson != ''">
                          <motDesICMS>
                            <xsl:value-of select="imposto/ICMS/ICMS99/motDesICMS" />
                          </motDesICMS>
                          <vICMSDeson>
                            <xsl:value-of select="imposto/ICMS/ICMS99/vICMSDeson" />
                          </vICMSDeson>
                        </xsl:if>
                      </ICMS70>
                    </xsl:if>
                    <!-- Fim ICMS70 -->
                    <!-- Início ICMS90 -->
                    <xsl:if test="number(imposto/ICMS/ICMS99/CST) = 90">
                      <ICMS90>
                        <orig>
                          <xsl:value-of select="imposto/ICMS/ICMS99/orig" />
                        </orig>
                        <CST>
                          <xsl:value-of select="imposto/ICMS/ICMS99/CST" />
                        </CST>
                        <xsl:if test="imposto/ICMS/ICMS99/modBC != ''">
                          <modBC>
                            <xsl:value-of select="imposto/ICMS/ICMS99/modBC" />
                          </modBC>
                          <vBC>
                            <xsl:value-of select="imposto/ICMS/ICMS99/vBC" />
                          </vBC>
                          <xsl:if test="imposto/ICMS/ICMS99/pRedBC != ''">
                            <pRedBC>
                              <xsl:value-of select="imposto/ICMS/ICMS99/pRedBC" />
                            </pRedBC>
                          </xsl:if>
                          <pICMS>
                            <xsl:value-of select="imposto/ICMS/ICMS99/pICMS" />
                          </pICMS>
                          <vICMS>
                            <xsl:value-of select="imposto/ICMS/ICMS99/vICMS" />
                          </vICMS>
                        </xsl:if>
                        <xsl:if test="imposto/ICMS/ICMS99/modBCST != ''">
                          <modBCST>
                            <xsl:value-of select="imposto/ICMS/ICMS99/modBCST" />
                          </modBCST>
                          <xsl:if test="imposto/ICMS/ICMS99/pMVAST != ''">
                          <pMVAST>
                            <xsl:value-of select="imposto/ICMS/ICMS99/pMVAST" />
                          </pMVAST>
                          </xsl:if>
                          <xsl:if test="imposto/ICMS/ICMS99/pRedBCST != '' and number(imposto/ICMS/ICMS99/pRedBCST) != 0">
                          <pRedBCST>
                            <xsl:value-of select="imposto/ICMS/ICMS99/pRedBCST" />
                          </pRedBCST>
                          </xsl:if>
                          <vBCST>
                            <xsl:value-of select="imposto/ICMS/ICMS99/vBCST" />
                          </vBCST>
                          <pICMSST>
                            <xsl:value-of select="imposto/ICMS/ICMS99/pICMSST" />
                          </pICMSST>
                          <vICMSST>
                            <xsl:value-of select="imposto/ICMS/ICMS99/vICMSST" />
                          </vICMSST>
                        <xsl:if test="imposto/ICMS/ICMS99/motDesICMS != '' and imposto/ICMS/ICMS99/vICMSDeson != ''">
                          <motDesICMS>
                            <xsl:value-of select="imposto/ICMS/ICMS99/motDesICMS" />
                          </motDesICMS>
                          <vICMSDeson>
                            <xsl:value-of select="imposto/ICMS/ICMS99/vICMSDeson" />
                          </vICMSDeson>
                        </xsl:if>
                        </xsl:if>
                      </ICMS90>
                    </xsl:if>
                    <!-- Fim ICMS90 -->
                    <!-- Início ICMSST -->
                    <xsl:if test="number(imposto/ICMS/ICMS99/vBCSTDest) > 0 or number(imposto/ICMS/ICMS99/vICMSSTDest > 0)">
                    <ICMSST>
                      <orig>
                        <xsl:value-of select="imposto/ICMS/ICMS99/orig" />
                      </orig>
                      <CST>
                        <xsl:value-of select="imposto/ICMS/ICMS99/CST" />
                      </CST>
                      <vBCSTRet>
                        <xsl:value-of select="imposto/ICMS/ICMS99/vBCSTRet" />
                      </vBCSTRet>
                      <vICMSSTRet>
                        <xsl:value-of select="imposto/ICMS/ICMS99/vICMSSTRet" />
                      </vICMSSTRet>
                      <vBCSTDest>
                        <xsl:value-of select="imposto/ICMS/ICMS99/vBCSTDest" />
                      </vBCSTDest>
                      <vICMSSTDest>
                        <xsl:value-of select="imposto/ICMS/ICMS99/vICMSSTDest" />
                      </vICMSSTDest>
                    </ICMSST>
                    </xsl:if>
                    <!-- Fim ICMSST -->
                    <!-- Início ICMSSN101 -->
                    <xsl:if test="number(imposto/ICMS/ICMS99/CSOSN) = 101">
			<ICMSSN101>
	                      <orig>
	                        <xsl:value-of select="imposto/ICMS/ICMS99/orig" />
	                      </orig>
	                      <CSOSN>
	                        <xsl:value-of select="imposto/ICMS/ICMS99/CSOSN" />
	                      </CSOSN>
	                      <pCredSN>
	                        <xsl:value-of select="imposto/ICMS/ICMS99/pCredSN" />
	                      </pCredSN>
	                      <vCredICMSSN>
	                        <xsl:value-of select="imposto/ICMS/ICMS99/vCredICMSSN" />
	                      </vCredICMSSN>
			</ICMSSN101>
                    </xsl:if>
                    <!-- Fim ICMSSN101 -->
                    <!-- Início ICMSSN102 - 102/103/300/400 -->
                    <xsl:if test="number(imposto/ICMS/ICMS99/CSOSN) = 102 or number(imposto/ICMS/ICMS99/CSOSN) = 103 or number(imposto/ICMS/ICMS99/CSOSN) = 300 or number(imposto/ICMS/ICMS99/CSOSN) = 400">
   		       <ICMSSN102>
                      <orig>
                        <xsl:value-of select="imposto/ICMS/ICMS99/orig" />
                      </orig>
                      <CSOSN>
                        <xsl:value-of select="imposto/ICMS/ICMS99/CSOSN" />
                      </CSOSN>
   		       </ICMSSN102>
                    </xsl:if>
                    <!-- Fim ICMSSN102 - 102/103/300/400 -->
                    <!-- ICMSSN201 -->
                    <xsl:if test="number(imposto/ICMS/ICMS99/CSOSN) = 201">
		<ICMSSN201>
                      <orig>
                        <xsl:value-of select="imposto/ICMS/ICMS99/orig" />
                      </orig>
                      <CSOSN>
                        <xsl:value-of select="imposto/ICMS/ICMS99/CSOSN" />
                      </CSOSN>
                      <modBCST>
                        <xsl:value-of select="imposto/ICMS/ICMS99/modBCST" />
                      </modBCST>
                      <xsl:if test="imposto/ICMS/ICMS99/pMVAST != '' and number(imposto/ICMS/ICMS99/pMVAST) != 0">
			                  <pMVAST>
                          <xsl:value-of select="imposto/ICMS/ICMS99/pMVAST" />
			                  </pMVAST>
                      </xsl:if>
                      <xsl:if test="imposto/ICMS/ICMS99/pRedBCST  != '' and number(imposto/ICMS/ICMS99/pRedBCST) != 0">
			                  <pRedBCST>
                          <xsl:value-of select="imposto/ICMS/ICMS99/pRedBCST" />
			                  </pRedBCST>
                      </xsl:if>
                      <vBCST>
                        <xsl:value-of select="imposto/ICMS/ICMS99/vBCST" />
                      </vBCST>
                      <pICMSST>
                        <xsl:value-of select="imposto/ICMS/ICMS99/pICMSST" />
                      </pICMSST>
                      <vICMSST>
                        <xsl:value-of select="imposto/ICMS/ICMS99/vICMSST" />
                      </vICMSST>
                      <pCredSN>
                        <xsl:value-of select="imposto/ICMS/ICMS99/pCredSN" />
                      </pCredSN>
                      <vCredICMSSN>
                        <xsl:value-of select="imposto/ICMS/ICMS99/vCredICMSSN" />
                      </vCredICMSSN>
		</ICMSSN201>
                    </xsl:if>
                    <!-- Fim ICMSSN201 -->
                    <!-- Início ICMSSN202 or ICMSSN203 -->
                    <xsl:if test="number(imposto/ICMS/ICMS99/CSOSN) = 202 or number(imposto/ICMS/ICMS99/CSOSN) = 203">
		<ICMSSN202>
                      <orig>
                        <xsl:value-of select="imposto/ICMS/ICMS99/orig" />
                      </orig>
                      <CSOSN>
                        <xsl:value-of select="imposto/ICMS/ICMS99/CSOSN" />
                      </CSOSN>
                      <modBCST>
                        <xsl:value-of select="imposto/ICMS/ICMS99/modBCST" />
                      </modBCST>
                      <xsl:if test="imposto/ICMS/ICMS99/pMVAST != '' and number(imposto/ICMS/ICMS99/pMVAST) != 0">
		                    <pMVAST>
                          <xsl:value-of select="imposto/ICMS/ICMS99/pMVAST" />
		                    </pMVAST>                      
		                  </xsl:if>
		                  <xsl:if test="imposto/ICMS/ICMS99/pRedBCST != '' and number(imposto/ICMS/ICMS99/pRedBCST) != 0">
		                    <pRedBCST>
                                      <xsl:value-of select="imposto/ICMS/ICMS99/pRedBCST" />
		                    </pRedBCST>
                      </xsl:if>
                      <vBCST>
                        <xsl:value-of select="imposto/ICMS/ICMS99/vBCST" />
                      </vBCST>
                      <pICMSST>
                        <xsl:value-of select="imposto/ICMS/ICMS99/pICMSST" />
                      </pICMSST>
                      <vICMSST>
                        <xsl:value-of select="imposto/ICMS/ICMS99/vICMSST" />
                      </vICMSST>
		</ICMSSN202>
                    </xsl:if>
                    <!-- Fim ICMSSN202 or ICMSSN203 -->
                    <!-- Início ICMSSN500 -->
                    <xsl:if test="number(imposto/ICMS/ICMS99/CSOSN) = 500">
    <ICMSSN500>
                      <orig>
                        <xsl:value-of select="imposto/ICMS/ICMS99/orig" />
                      </orig>
                      <CSOSN>
                        <xsl:value-of select="imposto/ICMS/ICMS99/CSOSN" />
                      </CSOSN>
 <xsl:if test="imposto/ICMS/ICMS99/vBCSTRet != ''">
                      <vBCSTRet>
                        <xsl:value-of select="imposto/ICMS/ICMS99/vBCSTRet" />
                      </vBCSTRet>
</xsl:if>
<xsl:if test="imposto/ICMS/ICMS99/vICMSSTRet != ''">
                      <vICMSSTRet>
                        <xsl:value-of select="imposto/ICMS/ICMS99/vICMSSTRet" />
                      </vICMSSTRet>
</xsl:if>
    </ICMSSN500>
                    </xsl:if>
<!-- Fim ICMSSN500 -->
<!-- Início ICMSSN900 -->
                    <xsl:if test="number(imposto/ICMS/ICMS99/CSOSN) = 900">
    <ICMSSN900>
                      <orig>
                        <xsl:value-of select="imposto/ICMS/ICMS99/orig" />
                      </orig>
                      <CSOSN>
                        <xsl:value-of select="imposto/ICMS/ICMS99/CSOSN" />
                      </CSOSN>
                      <xsl:if test="imposto/ICMS/ICMS99/modBC  != ''">
                        <modBC>
                          <xsl:value-of select="imposto/ICMS/ICMS99/modBC" />
                        </modBC>
                        <vBC>
                          <xsl:value-of select="imposto/ICMS/ICMS99/vBC" />
                        </vBC>
                        <xsl:if test="imposto/ICMS/ICMS99/pRedBC != '' and number(imposto/ICMS/ICMS99/pRedBC) != 0">
                          <pRedBC>
                            <xsl:value-of select="imposto/ICMS/ICMS99/pRedBC" />
                          </pRedBC>
                        </xsl:if>
                        <pICMS>
                          <xsl:value-of select="imposto/ICMS/ICMS99/pICMS" />
                        </pICMS>
                        <vICMS>
                          <xsl:value-of select="imposto/ICMS/ICMS99/vICMS" />
                        </vICMS>
                      </xsl:if>
                      <xsl:if test="imposto/ICMS/ICMS99/modBCST != ''">
                        <modBCST>
                          <xsl:value-of select="imposto/ICMS/ICMS99/modBCST" />
                        </modBCST>
                        <xsl:if test="imposto/ICMS/ICMS99/pMVAST != '' and number(imposto/ICMS/ICMS99/pMVAST) != 0">
                          <pMVAST>
                            <xsl:value-of select="imposto/ICMS/ICMS99/pMVAST" />
                          </pMVAST>
                        </xsl:if>
                        <xsl:if test="imposto/ICMS/ICMS99/pRedBCST != '' and number(imposto/ICMS/ICMS99/pRedBCST) != 0">
                          <pRedBCST>
                            <xsl:value-of select="imposto/ICMS/ICMS99/pRedBCST" />
                          </pRedBCST>
                        </xsl:if>
                        <vBCST>
                          <xsl:value-of select="imposto/ICMS/ICMS99/vBCST" />
                        </vBCST>
                        <pICMSST>
                          <xsl:value-of select="imposto/ICMS/ICMS99/pICMSST" />
                        </pICMSST>
                        <vICMSST>
                          <xsl:value-of select="imposto/ICMS/ICMS99/vICMSST" />
                        </vICMSST>
                      </xsl:if>
                      <xsl:if test="imposto/ICMS/ICMS99/pCredSN != ''">
                        <pCredSN>
                          <xsl:value-of select="imposto/ICMS/ICMS99/pCredSN" />
                        </pCredSN>
                      </xsl:if>
                      <xsl:if test="imposto/ICMS/ICMS99/vCredICMSSN != ''">
                        <vCredICMSSN>
                          <xsl:value-of select="imposto/ICMS/ICMS99/vCredICMSSN" />
                        </vCredICMSSN>
                      </xsl:if>
    </ICMSSN900>
                    </xsl:if>
<!-- Fim ICMSSN900 -->
                  </ICMS>
                  <xsl:if test="imposto/IPI/IPITrib/CST != ''">
                    <IPI>
                      <xsl:if test="imposto/IPI/clEnq != '' and imposto/IPI/clEnq != 0">
                        <clEnq>
                          <xsl:value-of select="imposto/IPI/clEnq"/>
                        </clEnq>
                      </xsl:if>
		                  <xsl:if test="imposto/IPI/CNPJProd != '' and number(imposto/IPI/CNPJProd) != 0">
                        <CNPJProd>
                          <xsl:value-of select="imposto/IPI/CNPJProd"/>
                        </CNPJProd>
                      </xsl:if>
                      <xsl:if test="imposto/IPI/cSelo != '' and number(imposto/IPI/cSelo) != 0 ">
                        <cSelo>
                          <xsl:value-of select="imposto/IPI/cSelo"/>
                        </cSelo>
                      </xsl:if>
                      <xsl:if test="imposto/IPI/qSelo != '' and number(imposto/IPI/qSelo) != 0">
                        <qSelo>
                          <xsl:value-of select="imposto/IPI/qSelo"/>
                        </qSelo>
                      </xsl:if>
                      <xsl:if test="imposto/IPI/cEnq != '' and imposto/IPI/cEnq != 0">
                        <cEnq>
                          <xsl:value-of select="imposto/IPI/cEnq"/>
                        </cEnq>
                      </xsl:if>
<!-- Início IPI CST 00 / 49 / 50 / 99 -->
                      <xsl:if test="imposto/IPI/IPITrib/CST != '' and (imposto/IPI/IPITrib/CST = '00' or imposto/IPI/IPITrib/CST = '49' or imposto/IPI/IPITrib/CST = '50' or imposto/IPI/IPITrib/CST = '99')">
                        <IPITrib>
                          <CST>
                            <xsl:value-of select="imposto/IPI/IPITrib/CST" />
                          </CST>
                          <xsl:if test="imposto/IPI/IPITrib/vBC != '' or imposto/IPI/IPITrib/pIPI != ''">
                            <vBC>
                              <xsl:value-of select="format-number(imposto/IPI/IPITrib/vBC, '#0.00') "/>
                            </vBC>
                            <pIPI>
                              <xsl:value-of select="format-number(imposto/IPI/IPITrib/pIPI , '#0.00') "/>
                            </pIPI>
                          </xsl:if>
                          <xsl:if test="imposto/IPI/IPITrib/qUnid != '' or imposto/IPI/IPITrib/vUnid != ''">
                            <qUnid>
                              <xsl:value-of select="format-number(imposto/IPI/IPITrib/qUnid, '#0.0000') "/>
                            </qUnid>
                            <vUnid>
                              <xsl:value-of select="format-number(imposto/IPI/IPITrib/vUnid, '#0.0000') "/>
                            </vUnid>
                          </xsl:if>
                          <vIPI>
                            <xsl:value-of select="format-number(imposto/IPI/IPITrib/vIPI, '#0.00') "/>
                          </vIPI>
                        </IPITrib>
                      </xsl:if>
<!-- Fim IPI CST 00 / 49 / 50 / 99 -->
<!-- Início IPI CST 01 / 02 / 03 / 04 / 05 / 51 / 52 / 53 / 54 / 55 -->
                      <xsl:if test="imposto/IPI/IPITrib/CST != '' and (imposto/IPI/IPITrib/CST = '01' or imposto/IPI/IPITrib/CST = '02' or imposto/IPI/IPITrib/CST = 03 or imposto/IPI/IPITrib/CST  = '04' or imposto/IPI/IPITrib/CST = '05' or imposto/IPI/IPITrib/CST = '51' or imposto/IPI/IPITrib/CST = '52' or imposto/IPI/IPITrib/CST = '53' or imposto/IPI/IPITrib/CST = '54' or imposto/IPI/IPITrib/CST = '55')">
                        <IPINT>
                          <CST>
                            <xsl:value-of select="format-number(imposto/IPI/IPITrib/CST,'00')"/>
                          </CST>
                        </IPINT>
                      </xsl:if>
<!-- Fim IPI CST 01 / 02 / 03 / 04 / 05 / 51 / 52 / 53 / 54 / 55 -->
                    </IPI>
                  </xsl:if>
<!-- Início II -->
		<xsl:if test="number(imposto/II/vBC) > 0 or number(imposto/II/vII) > 0">
                    <II>
                      <vBC>
                        <xsl:value-of select="imposto/II/vBC"/>
                      </vBC>
                      <vDespAdu>
                        <xsl:value-of select="imposto/II/vDespAdu"/>
                      </vDespAdu>
                      <vII>
                        <xsl:value-of select="imposto/II/vII"/>
                      </vII>
                      <vIOF>
                        <xsl:value-of select="imposto/II/vIOF"/>
                      </vIOF>
                    </II>
                  </xsl:if>
<!-- Fim II -->
                </xsl:otherwise>
              </xsl:choose>
<!-- Início PIS -->
              <PIS>
<!-- Início PIS  CST 01 | 02 -->
                <xsl:if test="imposto/PIS/PISPadr/CST= 01 or imposto/PIS/PISPadr/CST = 02">
                  <PISAliq>
                    <CST>
                      <xsl:value-of select="format-number(imposto/PIS/PISPadr/CST,'00')"/>
                    </CST>
                    <vBC>
                      <xsl:value-of select="translate(format-number(imposto/PIS/PISPadr/vBC,'#0.00'),',','.')"/>
                    </vBC>
                    <pPIS>
                      <xsl:value-of select="translate(format-number(imposto/PIS/PISPadr/pPIS,'#0.00'),',','.')"/>
                    </pPIS>
                    <vPIS>
                      <xsl:value-of select="translate(format-number(imposto/PIS/PISPadr/vPIS,'#0.00'),',','.')"/>
                    </vPIS>
                  </PISAliq>
                </xsl:if>
<!-- Fim PIS  CST 01 | 02 -->
<!-- Início PIS CST 03 -->
                <xsl:if test="imposto/PIS/PISPadr/CST = 03">
                  <PISQtde>
                    <CST>
                      <xsl:value-of select="format-number(imposto/PIS/PISPadr/CST,'00')"/>
                    </CST>
                    <qBCProd>
                      <xsl:value-of select="imposto/PIS/PISPadr/qBCProd"/>
                    </qBCProd>
                    <vAliqProd>
                      <xsl:value-of select="imposto/PIS/PISPadr/vAliqProd"/>
                    </vAliqProd>
                    <vPIS>
                      <xsl:value-of select="translate(format-number(imposto/PIS/PISPadr/vPIS,'#0.00'),',','.')"/>
                    </vPIS>
                  </PISQtde>
                </xsl:if>
<!-- Fim PIS CST 03 -->
<!-- Início PIS CST 04, 06, 07, 08 ou 09 -->
                <xsl:if test="imposto/PIS/PISPadr/CST = 04 or imposto/PIS/PISPadr/CST = 06 or imposto/PIS/PISPadr/CST = 07 or imposto/PIS/PISPadr/CST = 08 or imposto/PIS/PISPadr/CST = 09">
                  <PISNT>
                    <CST>
                      <xsl:value-of select="format-number(imposto/PIS/PISPadr/CST,'00')"/>
                    </CST>
                  </PISNT>
                </xsl:if>
<!-- Fim PIS CST 04, 06, 07, 08 ou 09 -->
<!-- Início - PIS outras operacoes, quando for diferente de 01, 02, 03, 04, 05, 06, 07, 08, 09 / maior que 10-->
                <xsl:if test="imposto/PIS/PISPadr/CST != '' and number(imposto/PIS/PISPadr/CST) > 10">
                  <PISOutr>
                    <CST>
                      <xsl:value-of select="format-number(imposto/PIS/PISPadr/CST,'00')"/>
                    </CST>

                    <xsl:if test="imposto/PIS/PISPadr/pPIS != '' and imposto/PIS/PISPadr/qBCProd = ''">
                      <vBC>
                        <xsl:value-of select="translate(format-number(imposto/PIS/PISPadr/vBC,'#0.00'),',','.')"/>
                      </vBC>
                      <pPIS>
                        <xsl:value-of select="translate(format-number(imposto/PIS/PISPadr/pPIS,'#0.00'),',','.')"/>
                      </pPIS>
                    </xsl:if>
                    
                    <xsl:if test="imposto/PIS/PISPadr/qBCProd != ''">
                      <qBCProd>
                        <xsl:value-of select="imposto/PIS/PISPadr/qBCProd"/>
                      </qBCProd>
                      <vAliqProd>
                        <xsl:value-of select="imposto/PIS/PISPadr/vAliqProd"/>
                      </vAliqProd>
                    </xsl:if>

                    <vPIS>
                      <xsl:value-of select="translate(format-number(imposto/PIS/PISPadr/vPIS,'#0.00'),',','.')"/>
                    </vPIS>
                  </PISOutr>
                </xsl:if>
<!-- Fim - PIS outras operacoes, quando for diferente de 01, 02, 03, 04, 05, 06, 07, 08, 09 / maior que 10-->
              </PIS>
<!-- Início - PIS ST -->
              <xsl:if test="imposto/PISST/vPIS != '' and number(imposto/PISST/vPIS) != 0 ">
                <PISST>
                  <xsl:if test="imposto/PISST/vBC != '' and number(imposto/PISST/vBC) != 0">
                    <vBC>
                      <xsl:value-of select="imposto/PISST/vBC"/>
                    </vBC>
                    <pPIS>
                      <xsl:value-of select="imposto/PISST/pPIS"/>
                    </pPIS>
                  </xsl:if>
                  <xsl:if test="imposto/PISST/qBCProd != '' and number(imposto/PISST/qBCProd) != 0">
                    <qBCProd>
                      <xsl:value-of select="imposto/PISST/qBCProd" />
                    </qBCProd>
                    <vAliqProd>
                      <xsl:value-of select="imposto/PISST/vAliqProd" />
                    </vAliqProd>
                  </xsl:if>
                  <vPIS>
                    <xsl:value-of select="imposto/PISST/vPIS"/>
                  </vPIS>
                </PISST>
              </xsl:if>
<!-- Fim - PIS ST -->
              <COFINS>
<!-- Início COFINS CST 01 ou 02 -->
                <xsl:if test="number(imposto/COFINS/COFINSPadr/CST) != 0 and (imposto/COFINS/COFINSPadr/CST = 01 or imposto/COFINS/COFINSPadr/CST = 02)">
                  <COFINSAliq>
                    <CST>
                      <xsl:value-of select="format-number(imposto/COFINS/COFINSPadr/CST,'00')"/>
                    </CST>
                    <vBC>
                      <xsl:value-of select="translate(format-number(imposto/COFINS/COFINSPadr/vBC,'#0.00'),',','.')"/>
                    </vBC>
                    <pCOFINS>
                      <xsl:value-of select="translate(format-number(imposto/COFINS/COFINSPadr/pCOFINS,'#0.00'),',','.')"/>
                    </pCOFINS>
                    <vCOFINS>
                      <xsl:value-of select="translate(format-number(imposto/COFINS/COFINSPadr/vCOFINS,'#0.00'),',','.')"/>
                    </vCOFINS>
                  </COFINSAliq>
                </xsl:if>
<!-- Fim COFINS CST 01 ou 02 -->
<!-- Início COFINS CST 03 -->
                <xsl:if test="number(imposto/COFINS/COFINSPadr/CST) != 0 and imposto/COFINS/COFINSPadr/CST = 03">
                  <COFINSQtde>
                    <CST>
                      <xsl:value-of select="format-number(imposto/COFINS/COFINSPadr/CST,'00')"/>
                    </CST>
                    <qBCProd>
                      <xsl:value-of select="imposto/COFINS/COFINSPadr/qBCProd"/>
                    </qBCProd>
                    <vAliqProd>
                      <xsl:value-of select="imposto/COFINS/COFINSPadr/vAliqProd"/>
                    </vAliqProd>
                    <vCOFINS>
                      <xsl:value-of select="translate(format-number(imposto/COFINS/COFINSPadr/vCOFINS,'#0.00'),',','.')"/>
                    </vCOFINS>
                  </COFINSQtde>
                </xsl:if>
<!-- Fim COFINS CST 03 -->
<!-- Início COFINS CST 04, 06, 07, 08 ou 09-->
                <xsl:if test="imposto/COFINS/COFINSPadr/CST = 04 or imposto/COFINS/COFINSPadr/CST = 06 or imposto/COFINS/COFINSPadr/CST = 07 or imposto/COFINS/COFINSPadr/CST = 08 or imposto/COFINS/COFINSPadr/CST = 09">
                  <COFINSNT>
                    <CST>
                      <xsl:value-of select="format-number(imposto/COFINS/COFINSPadr/CST,'00')"/>
                    </CST>
                  </COFINSNT>
                </xsl:if>
<!-- Fim COFINS CST 04, 06, 07, 08 ou 09-->
<!-- Início Cofins outras operacoes, quando for diferente de 01, 02, 03, 04, 05, 06, 07, 08, 09 / maior que 10 -->
                <xsl:if test="imposto/COFINS/COFINSPadr/CST != '' and number(imposto/COFINS/COFINSPadr/CST) > 10">
                  <COFINSOutr>
                    <CST>
                      <xsl:value-of select="format-number(imposto/COFINS/COFINSPadr/CST,'00')"/>
                    </CST>
                    <xsl:if test="imposto/COFINS/COFINSPadr/vBC != ''">
                      <vBC>
                        <xsl:value-of select="translate(format-number(imposto/COFINS/COFINSPadr/vBC,'#0.00'),',','.')"/>
                      </vBC>
                      <pCOFINS>
                        <xsl:value-of select="translate(format-number(imposto/COFINS/COFINSPadr/pCOFINS,'#0.00'),',','.')"/>
                      </pCOFINS>
                    </xsl:if>
                    <xsl:if test="imposto/COFINS/COFINSPadr/qBCProd != '' and imposto/COFINS/COFINSPadr/vBC = ''">
                      <qBCProd>
                        <xsl:value-of select="imposto/COFINS/COFINSPadr/qBCProd" />
                      </qBCProd>
                      <vAliqProd>
                        <xsl:value-of select="imposto/COFINS/COFINSPadr/vAliqProd" />
                      </vAliqProd>
                    </xsl:if>
                    <vCOFINS>
                      <xsl:value-of select="translate(format-number(imposto/COFINS/COFINSPadr/vCOFINS,'#0.00'),',','.')"/>
                    </vCOFINS>
                  </COFINSOutr>
                </xsl:if>
<!-- Fim Cofins outras operacoes, quando for diferente de 01, 02, 03, 04, 05, 06, 07, 08, 09 / maior que 10 -->
              </COFINS>
<!-- Fim COFINS -->
<!-- Início do cofins com ST -->
              <xsl:if test="(imposto/COFINSST/vBC != '' or imposto/COFINSST/vCOFINS != '') and number(imposto/COFINSST/COFINSST) = 1">
                <COFINSST>
                  <xsl:if test="imposto/COFINSST/vBC != ''">
                    <vBC>
                      <xsl:value-of select="imposto/COFINSST/vBC"/>
                    </vBC>
                    <pCOFINS>
                      <xsl:value-of select="imposto/COFINSST/pCOFINS"/>
                    </pCOFINS>
                  </xsl:if>
                  <xsl:if test="imposto/COFINSST/qBCProd != ''">
                    <qBCProd>
                      <xsl:value-of select="imposto/COFINSST/qBCProd"/>
                    </qBCProd>
                    <vAliqProd>
                      <xsl:value-of select="imposto/COFINSST/vAliqProd"/>
                    </vAliqProd>
                  </xsl:if>
                  <vCOFINS>
                    <xsl:value-of select="imposto/COFINSST/vCOFINS"/>
                  </vCOFINS>
                </COFINSST>
              </xsl:if>
<!-- Fim do cofins com ST -->
            </imposto>

            <xsl:if test="infAdProd != ''">
              <infAdProd>
                <xsl:value-of select="infAdProd"/>
              </infAdProd>
            </xsl:if>
          </det>
        </xsl:for-each>
        <!-- Fim Produtos -->
        <total>
          <ICMSTot>
            <xsl:if test="NFe/infNFe/total/ICMSTot/vBC = '' ">
              <vBC>0.00</vBC>
            </xsl:if>
            <xsl:if test="NFe/infNFe/total/ICMSTot/vBC != '' ">
              <vBC>
                <xsl:value-of select="translate(format-number(NFe/infNFe/total/ICMSTot/vBC,'#0.00'),',','.')"/>
              </vBC>
            </xsl:if>

            <xsl:if test="NFe/infNFe/total/ICMSTot/vICMS = ''">
              <vICMS>0.00</vICMS>
            </xsl:if>

            <xsl:if test="NFe/infNFe/total/ICMSTot/vICMS != ''">
              <vICMS>
                <xsl:value-of select="translate(format-number(NFe/infNFe/total/ICMSTot/vICMS,'#0.00'),',','.')"/>
              </vICMS>
            </xsl:if>
            <xsl:if test="NFe/infNFe/total/ICMSTot/vICMSDeson = '' ">
              <vICMSDeson>0.00</vICMSDeson>
            </xsl:if>
            <xsl:if test="NFe/infNFe/total/ICMSTot/vICMSDeson != '' ">
              <vICMSDeson>
                <xsl:value-of select="translate(format-number(NFe/infNFe/total/ICMSTot/vICMSDeson,'#0.00'),',','.')"/>
              </vICMSDeson>
            </xsl:if>
            <xsl:if test="NFe/infNFe/total/ICMSTot/vBCST = ''">
              <vBCST>0.00</vBCST>
            </xsl:if>
            <xsl:if test="NFe/infNFe/total/ICMSTot/vBCST != ''">
              <vBCST>
                <xsl:value-of select="translate(format-number(NFe/infNFe/total/ICMSTot/vBCST,'#0.00'),',','.')"/>
              </vBCST>
            </xsl:if>

            <xsl:if test="NFe/infNFe/total/ICMSTot/vST = ''">
              <vST>0.00</vST>
            </xsl:if>

            <xsl:if test="NFe/infNFe/total/ICMSTot/vST != ''">
              <vST>
                <xsl:value-of select="translate(format-number(NFe/infNFe/total/ICMSTot/vST,'#0.00'),',','.')"/>
              </vST>
            </xsl:if>

            <xsl:if test="NFe/infNFe/total/ICMSTot/vProd = '' ">
              <vProd>0.00</vProd>
            </xsl:if>

            <xsl:if test="NFe/infNFe/total/ICMSTot/vProd != '' ">
              <vProd>
                <xsl:value-of select="translate(format-number(NFe/infNFe/total/ICMSTot/vProd,'#0.00'),',','.')"/>
              </vProd>
            </xsl:if>

            <xsl:if test="NFe/infNFe/total/ICMSTot/vFrete = ''">
              <vFrete>0.00</vFrete>
            </xsl:if>

            <xsl:if test="NFe/infNFe/total/ICMSTot/vFrete != ''">
              <vFrete>
                <xsl:value-of select="translate(format-number(NFe/infNFe/total/ICMSTot/vFrete,'#0.00'),',','.')"/>
              </vFrete>
            </xsl:if>

            <xsl:if test="NFe/infNFe/total/ICMSTot/vSeg = ''">
              <vSeg>0.00</vSeg>
            </xsl:if>

            <xsl:if test="NFe/infNFe/total/ICMSTot/vSeg!= ''">
              <vSeg>
                <xsl:value-of select="translate(format-number(NFe/infNFe/total/ICMSTot/vSeg,'#0.00'),',','.')"/>
              </vSeg>
            </xsl:if>

            <xsl:if test="NFe/infNFe/total/ICMSTot/vDesc = ''">
              <vDesc>0.00</vDesc>
            </xsl:if>

            <xsl:if test="NFe/infNFe/total/ICMSTot/vDesc != ''">
              <vDesc>
                <xsl:value-of select="translate(format-number(NFe/infNFe/total/ICMSTot/vDesc,'#0.00'),',','.')"/>
              </vDesc>
            </xsl:if>

            <xsl:if test="NFe/infNFe/total/ICMSTot/vII = ''">
              <vII>0.00</vII>
            </xsl:if>

            <xsl:if test="NFe/infNFe/total/ICMSTot/vII != ''">
              <vII>
                <xsl:value-of select="translate(format-number(NFe/infNFe/total/ICMSTot/vII,'#0.00'),',','.')"/>
              </vII>
            </xsl:if>

            <xsl:if test="NFe/infNFe/total/ICMSTot/vIPI = ''">
              <vIPI>0.00</vIPI>
            </xsl:if>

            <xsl:if test="NFe/infNFe/total/ICMSTot/vIPI != ''">
              <vIPI>
                <xsl:value-of select="translate(format-number(NFe/infNFe/total/ICMSTot/vIPI,'#0.00'),',','.')"/>
              </vIPI>
            </xsl:if>

            <xsl:if test="NFe/infNFe/total/ICMSTot/vPIS = ''">
              <vPIS>0.00</vPIS>
            </xsl:if>

            <xsl:if test="NFe/infNFe/total/ICMSTot/vPIS != ''">
              <vPIS>
                <xsl:value-of select="translate(format-number(NFe/infNFe/total/ICMSTot/vPIS,'#0.00'),',','.')"/>
              </vPIS>
            </xsl:if>

            <xsl:if test="NFe/infNFe/total/ICMSTot/vCOFINS = ''">
              <vCOFINS>0.00</vCOFINS>
            </xsl:if>

            <xsl:if test="NFe/infNFe/total/ICMSTot/vCOFINS != ''">
              <vCOFINS>
                <xsl:value-of select="translate(format-number(NFe/infNFe/total/ICMSTot/vCOFINS,'#0.00'),',','.')"/>
              </vCOFINS>
            </xsl:if>

            <xsl:if test="NFe/infNFe/total/ICMSTot/vOutro = ''">
              <vOutro>0.00</vOutro>
            </xsl:if>

            <xsl:if test="NFe/infNFe/total/ICMSTot/vOutro != ''">
              <vOutro>
                <xsl:value-of select="translate(format-number(NFe/infNFe/total/ICMSTot/vOutro,'#0.00'),',','.')"/>
              </vOutro>
            </xsl:if>

            <xsl:if test="NFe/infNFe/total/ICMSTot/vNF != ''">
              <vNF>
                <xsl:value-of select="translate(format-number(NFe/infNFe/total/ICMSTot/vNF,'#0.00'),',','.')"/>
              </vNF>
            </xsl:if>
          </ICMSTot>
          <xsl:if test="total/ISSQNtot/vServ != '' or total/ISSQNtot/vISS != ''">
            <ISSQNtot>
              <vServ>
                <xsl:value-of select="total/ISSQNtot/vServ" />
              </vServ>
              <xsl:if test="total/ISSQNtot/vBC != ''">
                <vBC>
                  <xsl:value-of select="total/ISSQNtot/vBC" />
                </vBC>
              </xsl:if>
              <xsl:if test="total/ISSQNtot/vISS != ''">
                <vISS>
                  <xsl:value-of select="total/ISSQNtot/vISS" />
                </vISS>
              </xsl:if>
              <xsl:if test="total/ISSQNtot/vPIS != ''">
                <vPIS>
                  <xsl:value-of select="total/ISSQNtot/vPIS" />
                </vPIS>
              </xsl:if>
              <xsl:if test="total/ISSQNtot/vCOFINS != ''">
                <vCOFINS>
                  <xsl:value-of select="total/ISSQNtot/vCOFINS" />
                </vCOFINS>
              </xsl:if>
            </ISSQNtot>
          </xsl:if>
          <xsl:if test="total/retTrib/vRetPIS != '' or total/retTrib/vRetCOFINS != '' or total/retTrib/vRetCSLL != '' or total/retTrib/vBCIRRF != '' or total/retTrib/vIRRF != '' or total/retTrib/vBCRetPrev != '' or total/retTrib/vRetPrev != ''">
            <retTrib>
            <xsl:if test="total/retTrib/vRetPIS != ''">
              <vRetPIS>
                <xsl:value-of select="total/retTrib/vRetPIS" />
              </vRetPIS>
            </xsl:if>
            <xsl:if test="total/retTrib/vRetCOFINS != ''">
              <vRetCOFINS>
                <xsl:value-of select="total/retTrib/vRetCOFINS" />
              </vRetCOFINS>
            </xsl:if>
            <xsl:if test="total/retTrib/vRetCSLL != ''">
              <vRetCSLL>
                <xsl:value-of select="total/retTrib/vRetCSLL" />
              </vRetCSLL>
            </xsl:if>
            <xsl:if test="total/retTrib/vBCIRRF != ''">
              <vBCIRRF>
                <xsl:value-of select="total/retTrib/vBCIRRF" />
              </vBCIRRF>
            </xsl:if>
            <xsl:if test="total/retTrib/vIRRF != ''">
              <vIRRF>
                <xsl:value-of select="total/retTrib/vIRRF" />
              </vIRRF>
            </xsl:if>
            <xsl:if test="total/retTrib/vBCRetPrev != ''">
              <vBCRetPrev>
                <xsl:value-of select="total/retTrib/vBCRetPrev" />
              </vBCRetPrev>
            </xsl:if>
            <xsl:if test="total/retTrib/vRetPrev != ''">
              <vRetPrev>
                <xsl:value-of select="total/retTrib/vRetPrev" />
              </vRetPrev>
            </xsl:if>
            </retTrib>
          </xsl:if>
        </total>
        <transp>
          <modFrete>
            <xsl:value-of select="NFe/infNFe/transp/modFrete"/>
          </modFrete>
            <xsl:if test="NFe/infNFe/transp/transporta/xNome  != ''">
              <transporta>

                <xsl:if test="NFe/infNFe/transp/transporta/CNPJ != ''">
						      <CNPJ>
							    <xsl:value-of select="NFe/infNFe/transp/transporta/CNPJ"/>
						      </CNPJ>
    						</xsl:if>

            <xsl:if test="NFe/infNFe/transp/transporta/CPF != ''">
						  <CPF>
							<xsl:value-of select="NFe/infNFe/transp/transporta/CPF"/>
						  </CPF>
						</xsl:if>
						<xsl:if test="NFe/infNFe/transp/transporta/xNome != ''">
							<xNome>
							  <xsl:value-of select="NFe/infNFe/transp/transporta/xNome"/>
							</xNome>
						</xsl:if>
						<xsl:if test="NFe/infNFe/transp/transporta/IE != ''">
						  <IE>
							<xsl:value-of select="NFe/infNFe/transp/transporta/IE"/>
						  </IE>
						</xsl:if>
						<xsl:if test="NFe/infNFe/transp/transporta/xEnder != ''">
						  <xEnder>
							<xsl:value-of select="NFe/infNFe/transp/transporta/xEnder"/>
						  </xEnder>
						</xsl:if>
						<xsl:if test="NFe/infNFe/transp/transporta/xMun != ''">
						  <xMun>
							<xsl:value-of select="NFe/infNFe/transp/transporta/xMun"/>
						  </xMun>
						</xsl:if>
						<xsl:if test="NFe/infNFe/transp/transporta/UF != ''">
							<UF>
							  <xsl:value-of select="NFe/infNFe/transp/transporta/UF"/>
							</UF>
						</xsl:if>
              </transporta>
            </xsl:if>

            <xsl:if test="number(NFe/infNFe/transp/retTransp/vBCRet) != 0 and NFe/infNFe/transp/retTransp/vBCRet != '' ">
              <retTransp>
                <vServ>
                  <xsl:value-of select="NFe/infNFe/transp/retTransp/vServ"/>
                </vServ>
                <vBCRet>
                  <xsl:value-of select="NFe/infNFe/transp/retTransp/vBCRet"/>
                </vBCRet>
                <pICMSRet>
                  <xsl:value-of select="NFe/infNFe/transp/retTransp/pICMSRet"/>
                </pICMSRet>
                <vICMSRet>
                  <xsl:value-of select="NFe/infNFe/transp/retTransp/vICMSRet"/>
                </vICMSRet>
                <CFOP>
                  <xsl:value-of select="NFe/infNFe/transp/retTransp/CFOP"/>
                </CFOP>
                <cMunFG>
                  <xsl:value-of select="NFe/infNFe/transp/retTransp/cMunFG"/>
                </cMunFG>
              </retTransp>
            </xsl:if>

            <xsl:if test="normalize-space(NFe/infNFe/transp/veicTransp/placa) != '' or NFe/infNFe/transp/veicTransp/UF != ''  or NFe/infNFe/transp/veicTransp/RNTC != ''">
              <veicTransp>
                <placa>
                  <xsl:value-of select="NFe/infNFe/transp/veicTransp/placa"/>
                </placa>
                <UF>
                  <xsl:value-of select="NFe/infNFe/transp/veicTransp/UF"/>
                </UF>
                <xsl:if test="NFe/infNFe/transp/veicTransp/RNTC != ''">
                  <RNTC>
                    <xsl:value-of select="NFe/infNFe/transp/veicTransp/RNTC"/>
                  </RNTC>
                </xsl:if>
              </veicTransp>
            </xsl:if>

            <xsl:if test="normalize-space(NFe/infNFe/transp/reboque/placa) != ''">
              <reboque>
                <placa>
                  <xsl:value-of select="NFe/infNFe/transp/reboque/placa"/>
                </placa>
                <UF>
                  <xsl:value-of select="NFe/infNFe/transp/reboque/UF"/>
                </UF>
                <RNTC>
                  <xsl:value-of select="NFe/infNFe/transp/reboque/RNTC"/>
                </RNTC>
              </reboque>
            </xsl:if>

            <xsl:if test="NFe/infNFe/transp/vagao != ''">
              <vagao>
                <xsl:value-of select="NFe/infNFe/transp/vagao" />
              </vagao>
            </xsl:if>
            <xsl:if test="NFe/infNFe/transp/balsa != ''">
              <balsa>
                <xsl:value-of select="NFe/infNFe/transp/balsa" />
              </balsa>
            </xsl:if>

            <xsl:for-each select="NFe/infNFe/transp/vol">
	    <xsl:if test="number(qVol) > 0 or number(pesoL) > 0 or number(pesoB) > 0">
              <vol>
                <xsl:if test="number(qVol) > 0">
                  <qVol>
                    <xsl:value-of select="qVol"/>
                  </qVol>
                </xsl:if>
                <xsl:if test="esp != ''">
                  <esp>
                    <xsl:value-of select="esp"/>
                  </esp>
                </xsl:if>
                <xsl:if test="marca != ''">
                  <marca>
                    <xsl:value-of select="marca"/>
                  </marca>
                </xsl:if>
                <xsl:if test="nVol != ''">
                  <nVol>
                    <xsl:value-of select="nVol"/>
                  </nVol>
                </xsl:if>
                <xsl:if test="number(pesoL) > 0">
                  <pesoL>
                    <xsl:value-of select="format-number(number(pesoL),'#0.000')"/>
                  </pesoL>
                </xsl:if>
                <xsl:if test="number(pesoB) > 0">
                  <pesoB>
                    <xsl:value-of select="format-number(number(pesoB),'#0.000')"/>
                  </pesoB>
                </xsl:if>
                <xsl:if test="lacres/nLacre != ''">
                  <lacres>
                    <nLacre>
                      <xsl:value-of select="nLacre"/>
                    </nLacre>
                  </lacres>
                </xsl:if>
              </vol>
	    </xsl:if>
            </xsl:for-each>
			

        </transp>
        <cobr>
          <xsl:if test="NFe/infNFe/cobr/fat/nFat != ''">
            <fat>
              <nFat>
                <xsl:value-of select="NFe/infNFe/cobr/fat/nFat"/>
              </nFat>
              <xsl:if test="NFe/infNFe/cobr/fat/vOrig != 0">
                <vOrig>
                  <xsl:value-of select="translate(format-number(NFe/infNFe/cobr/fat/vOrig,'#0.00'),',','.')"/>
                </vOrig>
              </xsl:if>
              <xsl:if test="NFe/infNFe/cobr/fat/vDesc != 0">
                <vDesc>
                  <xsl:value-of select="translate(format-number(NFe/infNFe/cobr/fat/vDesc,'#0.00'),',','.')"/>
                </vDesc>
              </xsl:if>
              <xsl:if test="NFe/infNFe/cobr/fat/vliq != 0">
                <vLiq>
                  <xsl:value-of select="translate(format-number(NFe/infNFe/cobr/fat/vLiq,'#0.00'),',','.')"/>
                </vLiq>
              </xsl:if>
            </fat>
          </xsl:if>

          <xsl:for-each select="NFe/infNFe/cobr/dup">
            <xsl:if test="nDup != '' and vDup != '' and nDup !=0 and vDup != 0">
              <dup>
                <nDup>
                  <xsl:value-of select="nDup"/>
                </nDup>
                <dVenc>
                  <xsl:value-of select="dVenc"/>
                </dVenc>
                <vDup>
                  <xsl:value-of select="translate(format-number(vDup,'#0.00'),',','.')"/>
                </vDup>
              </dup>
            </xsl:if>
          </xsl:for-each>
        </cobr>
        <xsl:if test="NFe/infNFe/infAdic/infAdFisco != '' or NFe/infNFe/infAdic/infCpl != ''">
          <infAdic>
            <xsl:if test="NFe/infNFe/infAdic/infAdFisco != ''">
              <infAdFisco>
                <xsl:value-of select="NFe/infNFe/infAdic/infAdFisco"/>
              </infAdFisco>
            </xsl:if>
            <xsl:if test="NFe/infNFe/infAdic/infCpl != ''">
              <infCpl>
                <xsl:value-of select="NFe/infNFe/infAdic/infCpl"/>
              </infCpl>
            </xsl:if>
          </infAdic>
        </xsl:if>
        <xsl:if test="NFe/infNFE/compra/xCont != ''">
          <compra>
            <xCont>
              <xsl:value-of select="NFe/infNFe/compra/xCont"/>
            </xCont>
          </compra>
        </xsl:if>

<xsl:if test="NFe/infNFe/exporta/UFSaidaPais != '' or NFe/infNFe/exporta/xLocExporta != ''">
				<exporta>
				<xsl:if test="NFe/infNFe/exporta/UFSaidaPais != ''">
					<UFSaidaPais>
						<xsl:value-of select="NFe/infNFe/exporta/UFSaidaPais"/>
					</UFSaidaPais>
				</xsl:if>
				<xsl:if test="NFe/infNFe/exporta/xLocExporta != ''">
					<xLocExporta>
						<xsl:value-of select="NFe/infNFe/exporta/xLocExporta"/>
					</xLocExporta>
				<xsl:if test="NFe/infNFe/exporta/xLocDespacho != ''">
				</xsl:if>
					<xLocDespacho>
						<xsl:value-of select="NFe/infNFe/exporta/xLocDespacho"/>
					</xLocDespacho>
				</xsl:if>
				</exporta>
  </xsl:if>

      </infNFe>
    </NFe>
  </xsl:template>
</xsl:stylesheet>