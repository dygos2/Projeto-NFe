<?xml version='1.0' encoding='utf-8' ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns="http://www.portalfiscal.inf.br/nfe" xmlns:a="http://www.portalfiscal.inf.br/nfe">
	<xsl:output method="xml" encoding="UTF-8" indent="yes"/>
	<xsl:template match="/">
		<NFe>
			<infNFe versao="1.10">
				<xsl:attribute name="Id">
					<xsl:value-of select="enviNFe/NFe/infNFe/@Id"/>
				</xsl:attribute>

				<ide>
					<cUF>
						<xsl:value-of select="enviNFe/NFe/infNFe/ide/cUF"/>
					</cUF>
					<cNF>
						<xsl:value-of select="format-number(enviNFe/NFe/infNFe/ide/cNF,'000000000')"/>
					</cNF>
					<natOp>
						<xsl:value-of select="enviNFe/NFe/infNFe/ide/natOp"/>
					</natOp>
					<indPag>
						<xsl:value-of select="enviNFe/NFe/infNFe/ide/indPag"/>
					</indPag>
					<mod>
						<xsl:value-of select="enviNFe/NFe/infNFe/ide/mod"/>
					</mod>
					<serie>            
              <xsl:value-of select="number(enviNFe/NFe/infNFe/ide/serie)"/>
					</serie>
					<nNF>
						<xsl:value-of select="number(enviNFe/NFe/infNFe/ide/nNF)"/>
					</nNF>
					<dEmi>
						<xsl:value-of select="enviNFe/NFe/infNFe/ide/dEmi"/>
					</dEmi>
					<dSaiEnt>
						<xsl:value-of select="enviNFe/NFe/infNFe/ide/dSaiEnt"/>
					</dSaiEnt>
					<tpNF>
						<xsl:value-of select="number(enviNFe/NFe/infNFe/ide/tpNF)"/>
					</tpNF>
					<cMunFG>
						<xsl:value-of select="enviNFe/NFe/infNFe/ide/cMunFG"/>
					</cMunFG>
					<tpImp>
						<xsl:value-of select="number(enviNFe/NFe/infNFe/ide/tpImp)"/>
					</tpImp>
					<tpEmis>
						<xsl:value-of select="enviNFe/NFe/infNFe/ide/tpEmis"/>
					</tpEmis>
					<cDV>
						<xsl:value-of select="enviNFe/NFe/infNFe/ide/cDV"/>
					</cDV>
					<tpAmb>1</tpAmb>
					<finNFe>1</finNFe>
					<procEmi>0</procEmi>
					<verProc>1.10</verProc>
				</ide>
				<emit>
					<CNPJ>
						<xsl:value-of select="enviNFe/NFe/infNFe/emit/CNPJ"/>
					</CNPJ>
					<xNome>
						<xsl:value-of select="enviNFe/NFe/infNFe/emit/xNome"/>
					</xNome>
					<xFant>
						<xsl:value-of select="enviNFe/NFe/infNFe/emit/xFant"/>
					</xFant>
					<enderEmit>
						<xLgr>
							<xsl:value-of select="enviNFe/NFe/infNFe/emit/enderEmit/xLgr"/>
						</xLgr>
						<nro>
							<xsl:value-of select="enviNFe/NFe/infNFe/emit/enderEmit/nro"/>
						</nro>
            <xsl:if test="enviNFe/NFe/infNFe/emit/enderEmit/xCpl != ''">
              <xCpl>
                <xsl:value-of select="enviNFe/NFe/infNFe/emit/enderEmit/xCpl"/>
              </xCpl>
            </xsl:if>

            <xsl:if test="enviNFe/NFe/infNFe/emit/enderEmit/xBairro != ''">
              <xBairro>
                <xsl:value-of select="enviNFe/NFe/infNFe/emit/enderEmit/xBairro"/>
              </xBairro>
            </xsl:if>
            <xsl:if test="enviNFe/NFe/infNFe/emit/enderEmit/xBairro = ''">
              <xBairro>-</xBairro >
            </xsl:if>
						<cMun>
							<xsl:value-of select="enviNFe/NFe/infNFe/emit/enderEmit/cMun"/>
						</cMun>
						<xMun>
							<xsl:value-of select="enviNFe/NFe/infNFe/emit/enderEmit/xMun"/>
						</xMun>
						<UF>
							<xsl:value-of select="enviNFe/NFe/infNFe/emit/enderEmit/UF"/>
						</UF>
						<CEP>
							<xsl:value-of select="enviNFe/NFe/infNFe/emit/enderEmit/CEP"/>
						</CEP>
						<cPais>
							<xsl:value-of select="enviNFe/NFe/infNFe/emit/enderEmit/cPais"/>
						</cPais>
						<xPais>
							<xsl:value-of select="enviNFe/NFe/infNFe/emit/enderEmit/xPais"/>
						</xPais>
            <xsl:if test="enviNFe/NFe/infNFe/emit/enderEmit/fone != ''">
              <fone>
                <xsl:value-of select="number(enviNFe/NFe/infNFe/emit/enderEmit/fone)"/>
              </fone>
            </xsl:if>
					</enderEmit>
					<IE>
						<xsl:value-of select="enviNFe/NFe/infNFe/emit/IE"/>
					</IE>
					<xsl:if test="enviNFe/NFe/infNFe/emit/IM != ''">
						<IM>
							<xsl:value-of select="enviNFe/NFe/infNFe/emit/IM"/>
						</IM>
					</xsl:if>
				</emit>
				<dest>
					<xsl:if test="enviNFe/NFe/infNFe/dest/CNPJ != ''">
						<CNPJ>
							<xsl:value-of select="enviNFe/NFe/infNFe/dest/CNPJ"/>
						</CNPJ>
					</xsl:if>
					<xsl:if test="enviNFe/NFe/infNFe/dest/CPF != ''">
						<CPF>
							<xsl:value-of select="translate(enviNFe/NFe/infNFe/dest/CPF,'-','')"/>
						</CPF>
					</xsl:if>
					<xNome>
						<xsl:value-of select="enviNFe/NFe/infNFe/dest/xNome"/>
					</xNome>
					<enderDest>
						<xLgr>
							<xsl:value-of select="enviNFe/NFe/infNFe/dest/enderDest/xLgr"/>
						</xLgr>
						<nro>
							<xsl:value-of select="enviNFe/NFe/infNFe/dest/enderDest/nro"/>
						</nro>
            <xsl:if test="enviNFe/NFe/infNFe/dest/enderDest/xCpl != ''">
              <xCpl>
                <xsl:value-of select="enviNFe/NFe/infNFe/dest/enderDest/xCpl"/>
              </xCpl>
            </xsl:if>
            <xsl:if test="enviNFe/NFe/infNFe/dest/enderDest/xBairro != ''">
              <xBairro>
                <xsl:value-of select="enviNFe/NFe/infNFe/dest/enderDest/xBairro"/>
              </xBairro>
            </xsl:if>
            <xsl:if test="enviNFe/NFe/infNFe/dest/enderDest/xBairro = ''">
              <xBairro>-</xBairro>
            </xsl:if>
						<cMun>
							<xsl:value-of select="enviNFe/NFe/infNFe/dest/enderDest/cMun"/>
						</cMun>
						<xMun>
							<xsl:value-of select="enviNFe/NFe/infNFe/dest/enderDest/xMun"/>
						</xMun>
						<UF>
							<xsl:value-of select="enviNFe/NFe/infNFe/dest/enderDest/UF"/>
						</UF>
						<CEP>
							<xsl:value-of select="enviNFe/NFe/infNFe/dest/enderDest/CEP"/>
						</CEP>
						<cPais>1058</cPais>
						<xPais>
							<xsl:value-of select="enviNFe/NFe/infNFe/dest/enderDest/xPais"/>
						</xPais>
            <xsl:if test="enviNFe/NFe/infNFe/dest/enderDest/fone != ''">
              <fone>
                <xsl:value-of select="enviNFe/NFe/infNFe/dest/enderDest/fone"/>
              </fone>
            </xsl:if>
					</enderDest>
					<IE>
						<xsl:value-of select="enviNFe/NFe/infNFe/dest/IE"/>
					</IE>
				</dest>
				<xsl:if test="enviNFe/NFe/infNFe/retirada/CNPJ != ''"> 
				<retirada>
					<CNPJ>
						<xsl:value-of select="enviNFe/NFe/infNFe/retirada/CNPJ"/>
					</CNPJ>
					<xLgr>
						<xsl:value-of select="enviNFe/NFe/infNFe/retirada/xLgr"/>
					</xLgr>
					<nro>
						<xsl:value-of select="enviNFe/NFe/infNFe/retirada/nro"/>
					</nro>
          <xsl:if test="enviNFe/NFe/infNFe/retirada/xCpl != ''">
            <xCpl>
              <xsl:value-of select="enviNFe/NFe/infNFe/retirada/xCpl"/>
            </xCpl>
          </xsl:if>

          <xsl:if test="enviNFe/NFe/infNFe/retirada/xBairro != ''">
            <xBairro>
              <xsl:value-of select="enviNFe/NFe/infNFe/retirada/xBairro"/>
            </xBairro>
          </xsl:if>
          <xsl:if test="enviNFe/NFe/infNFe/retirada/xBairro = ''">
            <xBairro>-</xBairro>
          </xsl:if >
          
					<cMun>
						<xsl:value-of select="enviNFe/NFe/infNFe/retirada/cMun"/>
					</cMun>
					<xMun>
						<xsl:value-of select="enviNFe/NFe/infNFe/retirada/xMun"/>
					</xMun>
					<UF>
						<xsl:value-of select="enviNFe/NFe/infNFe/retirada/UF"/>
					</UF>
				</retirada>
				</xsl:if>
				<xsl:if test="enviNFe/NFe/infNFe/entrega/CNPJ != ''">
				<entrega>
					<CNPJ>
						<xsl:value-of select="enviNFe/NFe/infNFe/entrega/CNPJ"/>
					</CNPJ>
					<xLgr>
						<xsl:value-of select="enviNFe/NFe/infNFe/entrega/xLgr"/>
					</xLgr>
					<nro>
						<xsl:value-of select="enviNFe/NFe/infNFe/entrega/nro"/>
					</nro>
          <xsl:if test="enviNFe/NFe/infNFe/entrega/xCpl != ''">
            <xCpl>
              <xsl:value-of select="enviNFe/NFe/infNFe/entrega/xCpl"/>
            </xCpl>
          </xsl:if>
          <xsl:if test="enviNFe/NFe/infNFe/entrega/xBairro != ''">
            <xBairro>
              <xsl:value-of select="enviNFe/NFe/infNFe/entrega/xBairro"/>
            </xBairro>
          </xsl:if>
          <xsl:if test="enviNFe/NFe/infNFe/entrega/xBairro = ''">
            <xBairro>-</xBairro>
          </xsl:if>

          <cMun>
						<xsl:value-of select="enviNFe/NFe/infNFe/entrega/cMun"/>
					</cMun>
					<xMun>
						<xsl:value-of select="enviNFe/NFe/infNFe/entrega/xMun"/>
					</xMun>
					<UF>
						<xsl:value-of select="enviNFe/NFe/infNFe/entrega/UF"/>
					</UF>
				</entrega>
				</xsl:if>
				<xsl:for-each select="enviNFe/NFe/infNFe/det">
					<det>
						<xsl:attribute name="nItem">
							<xsl:value-of select="@nItem"/>
						</xsl:attribute>
						<prod>
							<cProd>
								<xsl:value-of select="prod/cProd"/>
							</cProd>
							<cEAN>
								<xsl:value-of select="prod/cEAN"/>
							</cEAN>
							<xProd>
								<xsl:value-of select="prod/xProd"/>
							</xProd>
              <xsl:if test="prod/NCM != ''">
                <NCM>
                  <xsl:value-of select="prod/NCM"/>
                </NCM>
              </xsl:if>
							<xsl:if test="prod/genero != ''">
								<genero>
									<xsl:value-of select="prod/genero"/>
								</genero>
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
                  <xsl:value-of select="translate(format-number(prod/vProd div prod/qCom,'#0.0000'),',','.')"/>
                </vUnCom>
              </xsl:if>
              <xsl:if test="prod/vUnCom">
                <vUnCom>
                  <xsl:value-of select="format-number(prod/vUnCom,'#0.0000')"/>
                </vUnCom>
              </xsl:if>
              
              
							<vProd>
								<xsl:value-of select="format-number(number(prod/vProd),'#0.00')"/>
							</vProd>
							<cEANTrib>
								<xsl:value-of select="prod/cEAN"/>
							</cEANTrib>

              
              <xsl:if test="prod/uTrib!=''">
                <uTrib>
                  <xsl:value-of select="prod/uTrib"/>
                </uTrib>
              </xsl:if>
              <xsl:if test="prod/uTrib=''">
                <uTrib>-</uTrib>
              </xsl:if>


              <qTrib>
								<xsl:value-of select="translate(format-number(prod/qTrib,'#0.0000'),',','.')"/>
							</qTrib>
							<vUnTrib>
								<xsl:value-of select="translate(format-number(prod/vProd div prod/qCom,'#0.0000'),',','.')"/>
							</vUnTrib>
							<xsl:if test="prod/vFrete != 0">
								<vFrete>
									<xsl:value-of select="prod/vFrete"/>
								</vFrete>
							</xsl:if>
							<xsl:if test="prod/vSeg != 0">
								<vSeg>
									<xsl:value-of select="prod/vSeg"/>
								</vSeg>
							</xsl:if>
							<xsl:if test="prod/vDesc != 0">
								<vDesc>
									<xsl:value-of select="prod/vDesc"/>
								</vDesc>
							</xsl:if>
              <xsl:if test="prod/comb/cProdANP !='' ">
              <xsl:if test="number(prod/comb/cProdANP) != 0">
                
                <comb>
                  <cProdANP>
                    <xsl:value-of select="prod/comb/cProdANP"/>
                  </cProdANP>
                  <CODIF>
                    <xsl:value-of select="prod/comb/CODIF"/>
                  </CODIF>

                  <qTemp>
                    <xsl:value-of select="prod/comb/qTemp"/>
                  </qTemp>

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
                  
                  <ICMSComb>
                    <vBCICMS>
                      <xsl:value-of select="prod/comb/ICMSComb/vBCICMS"/>
                    </vBCICMS>
                    <vICMS>
                      <xsl:value-of select="prod/comb/ICMSComb/vICMS"/>
                    </vICMS>
                    <vBCICMSST>
                      <xsl:value-of select="prod/comb/ICMSComb/vBCICMSST"/>
                    </vBCICMSST>
                    <vICMSST>
                      <xsl:value-of select="prod/comb/ICMSComb/vICMSST"/>
                    </vICMSST>
                  </ICMSComb>
                  <ICMSInter>
                    <vBCICMSSTDest>
                      <xsl:value-of select="prod/comb/ICMSInter/vBCICMSSTDest"/>
                    </vBCICMSSTDest>
                    <vICMSSTDest>
                      <xsl:value-of select="prod/comb/ICMSInter/vICMSSTDest"/>
                    </vICMSSTDest>
                  </ICMSInter>
                  <ICMSCons>
                    <vBCICMSSTCons>
                      <xsl:value-of select="prod/comb/ICMSCons/vBCICMSSTCons"/>
                    </vBCICMSSTCons>
                    <vICMSSTCons>
                      <xsl:value-of select="prod/comb/ICMSCons/vICMSSTCons"/>
                    </vICMSSTCons>
                    <UFCons>
                      <xsl:value-of select="prod/comb/ICMSCons/UFcons"/>
                    </UFCons>
                  </ICMSCons>
                </comb>
              </xsl:if>
              </xsl:if>
              
						</prod>
						<imposto>
							<ICMS>
								<xsl:if test="imposto/ICMS/ICMS00">
									<ICMS00>
										<orig>
											<xsl:value-of select="imposto/ICMS/ICMS00/orig"/>
										</orig>
										<CST>
											<xsl:value-of select="format-number(imposto/ICMS/ICMS00/CST,'00')"/>
										</CST>
										<modBC>
											<xsl:value-of select="imposto/ICMS/ICMS00/modBC"/>
										</modBC>
										<vBC>
											<xsl:value-of select="imposto/ICMS/ICMS00/vBC"/>
										</vBC>
										<pICMS>
											<xsl:value-of select="imposto/ICMS/ICMS00/pICMS"/>
										</pICMS>
										<vICMS>
											<xsl:value-of select="imposto/ICMS/ICMS00/vICMS"/>
										</vICMS>
									</ICMS00>
								</xsl:if>
								<xsl:if test="imposto/ICMS/ICMS10">
									<ICMS10>
										<orig>
											<xsl:value-of select="imposto/ICMS/ICMS10/orig"/>
										</orig>
										<CST>
											<xsl:value-of select="format-number(imposto/ICMS/ICMS10/CST,'00')"/>
										</CST>
										<modBC>
											<xsl:value-of select="imposto/ICMS/ICMS10/modBC"/>
										</modBC>
										<vBC>
											<xsl:value-of select="imposto/ICMS/ICMS10/vBC"/>
										</vBC>
										<pICMS>
											<xsl:value-of select="imposto/ICMS/ICMS10/pICMS"/>
										</pICMS>
										<vICMS>
											<xsl:value-of select="imposto/ICMS/ICMS10/vICMS"/>
										</vICMS>
										<modBCST>
											<xsl:value-of select="imposto/ICMS/ICMS10/modBCST"/>
										</modBCST>
										<vBCST>
											<xsl:value-of select="imposto/ICMS/ICMS10/vBCST"/>
										</vBCST>
										<pICMSST>
											<xsl:value-of select="imposto/ICMS/ICMS10/pICMSST"/>
										</pICMSST>
										<vICMSST>
											<xsl:value-of select="imposto/ICMS/ICMS10/vICMSST"/>
										</vICMSST>
									</ICMS10>
								</xsl:if>
								<xsl:if test="imposto/ICMS/ICMS20">
									<ICMS20>
										<orig>
											<xsl:value-of select="imposto/ICMS/ICMS20/orig"/>
										</orig>
										<CST>
											<xsl:value-of select="format-number(imposto/ICMS/ICMS20/CST,'00')"/>
										</CST>
										<modBC>
											<xsl:value-of select="imposto/ICMS/ICMS20/modBC"/>
										</modBC>
										<pRedBC>
											<xsl:value-of select="imposto/ICMS/ICMS20/pRedBC"/>
										</pRedBC>
										<vBC>
											<xsl:value-of select="imposto/ICMS/ICMS20/vBC"/>
										</vBC>
										<pICMS>
											<xsl:value-of select="imposto/ICMS/ICMS20/pICMS"/>
										</pICMS>
										<vICMS>
											<xsl:value-of select="imposto/ICMS/ICMS20/vICMS"/>
										</vICMS>
									</ICMS20>
								</xsl:if>
								<xsl:if test="imposto/ICMS/ICMS40">
									<ICMS40>
										<orig>
											<xsl:value-of select="imposto/ICMS/ICMS40/orig"/>
										</orig>
										<CST>
											<xsl:value-of select="format-number(imposto/ICMS/ICMS40/CST,'00')"/>
										</CST>
									</ICMS40>
								</xsl:if>
								<xsl:if test="imposto/ICMS/ICMS60">
									<ICMS60>
										<orig>
											<xsl:value-of select="imposto/ICMS/ICMS60/orig"/>
										</orig>
										<CST>
											<xsl:value-of select="format-number(imposto/ICMS/ICMS60/CST,'00')"/>
										</CST>
                    
                    <xsl:if test="number(imposto/ICMS/ICMS60/vBCST)=0">
                      <vBCST>0</vBCST>
                    </xsl:if>
                    <xsl:if test="number(imposto/ICMS/ICMS60/vBCST)!=0">
                    <vBCST>
                        <xsl:value-of select="imposto/ICMS/ICMS60/vBCST"/>
                      </vBCST>
                    </xsl:if>

                    <xsl:if test="number(imposto/ICMS/ICMS60/vICMSST)=0">
                      <vICMSST>0</vICMSST>
                    </xsl:if>
                    <xsl:if test="number(imposto/ICMS/ICMS60/vICMSST)!=0">
                      <vICMSST>
                        <xsl:value-of select="imposto/ICMS/ICMS60/vICMSST"/>
                      </vICMSST>
                    </xsl:if>
                      
									</ICMS60>
								</xsl:if>
							</ICMS>
              <xsl:if test="imposto/IPI/cEnq">
                <IPI>
                  <cEnq>
                    <xsl:value-of select="imposto/IPI/cEnq"/>
                  </cEnq>
                  <IPINT>
                    <CST>
                      <xsl:value-of select="imposto/IPI/IPINT/CST"/>
                    </CST>
                  </IPINT>
                </IPI>
              </xsl:if>

              <xsl:if test="imposto/PIS/PISAliq/CST != ''">
							<PIS>
								<xsl:if test="imposto/PIS/PISAliq!='' and number(imposto/PIS/PISAliq/CST)!=0">
									<PISAliq>
										<CST>
											<xsl:value-of select="format-number(imposto/PIS/PISAliq/CST,'00')"/>
										</CST>
										<vBC>
											<xsl:value-of select="translate(format-number(imposto/PIS/PISAliq/vBC,'#0.00'),',','.')"/>
										</vBC>
										<pPIS>
											<xsl:value-of select="translate(format-number(imposto/PIS/PISAliq/pPIS,'#0.00'),',','.')"/>
										</pPIS>
										<vPIS>
											<xsl:value-of select="translate(format-number(imposto/PIS/PISAliq/vPIS,'#0.00'),',','.')"/>
										</vPIS>
									</PISAliq>
								</xsl:if>
								<xsl:if test="imposto/PIS/PISNT='' and number(imposto/PIS/PISNT/CST)!=0">
									<PISNT>
										<CST>
											<xsl:value-of select="format-number(imposto/PIS/PISNT/CST,'00')"/>
										</CST>
									</PISNT>
								</xsl:if>
                <xsl:if test="imposto/PIS/PISAliq!='' and number(imposto/PIS/PISAliq/CST)=0">                  
                  <PISNT>
                    <CST>08</CST>
                  </PISNT>
                </xsl:if>
							</PIS>
              </xsl:if>

              <xsl:if test="imposto/COFINS/COFINSAliq/CST != '' ">
              <COFINS>
                <xsl:if test="imposto/COFINS/COFINSAliq !='' and number(imposto/COFINS/COFINSAliq/CST)!=0  ">
                  <COFINSAliq>
                    <CST>
                      <xsl:value-of select="format-number(imposto/COFINS/COFINSAliq/CST,'00')"/>
                    </CST>
                    <vBC>
                      <xsl:value-of select="translate(format-number(imposto/COFINS/COFINSAliq/vBC,'#0.00'),',','.')"/>
                    </vBC>
                    <pCOFINS>
                      <xsl:value-of select="translate(format-number(imposto/COFINS/COFINSAliq/pCOFINS,'#0.00'),',','.')"/>
                    </pCOFINS>
                    <vCOFINS>
                      <xsl:value-of select="translate(format-number(imposto/COFINS/COFINSAliq/vCOFINS,'#0.00'),',','.')"/>
                    </vCOFINS>
                  </COFINSAliq>
                </xsl:if>
                <xsl:if test="imposto/COFINS/COFINSNT/CST !='' and number(imposto/COFINS/COFINSNT/CST)!=0">
                  <COFINSNT>
                    <CST>
                      <xsl:value-of select="format-number(imposto/COFINS/COFINSNT/CST,'00')"/>
                    </CST>
                  </COFINSNT>
                </xsl:if>
                <xsl:if test="imposto/COFINS/COFINSAliq !='' and number(imposto/COFINS/COFINSAliq/CST)=0">
                  <COFINSNT>
                    <CST>08</CST>
                  </COFINSNT>
                </xsl:if>
                  
              </COFINS>
              </xsl:if>
              
						</imposto>
					</det>
				</xsl:for-each>
				<total>
					<ICMSTot>
						<vBC>
							<xsl:value-of select="translate(format-number(enviNFe/NFe/infNFe/total/ICMSTot/vBC,'#0.00'),',','.')"/>
						</vBC>
						<vICMS>
							<xsl:value-of select="translate(format-number(enviNFe/NFe/infNFe/total/ICMSTot/vICMS,'#0.00'),',','.')"/>
						</vICMS>
						<vBCST>
							<xsl:value-of select="translate(format-number(enviNFe/NFe/infNFe/total/ICMSTot/vBCST,'#0.00'),',','.')"/>
						</vBCST>
						<vST>
							<xsl:value-of select="translate(format-number(enviNFe/NFe/infNFe/total/ICMSTot/vST,'#0.00'),',','.')"/>
						</vST>
						<vProd>
							<xsl:value-of select="translate(format-number(enviNFe/NFe/infNFe/total/ICMSTot/vProd,'#0.00'),',','.')"/>
						</vProd>
						<vFrete>
							<xsl:value-of select="translate(format-number(enviNFe/NFe/infNFe/total/ICMSTot/vFrete,'#0.00'),',','.')"/>
						</vFrete>
						<vSeg>
							<xsl:value-of select="translate(format-number(enviNFe/NFe/infNFe/total/ICMSTot/vSeg,'#0.00'),',','.')"/>
						</vSeg>
						<vDesc>
							<xsl:value-of select="translate(format-number(enviNFe/NFe/infNFe/total/ICMSTot/vDesc,'#0.00'),',','.')"/>
						</vDesc>
						<vII>
							<xsl:value-of select="translate(format-number(enviNFe/NFe/infNFe/total/ICMSTot/vII,'#0.00'),',','.')"/>
						</vII>
						<vIPI>
							<xsl:value-of select="translate(format-number(enviNFe/NFe/infNFe/total/ICMSTot/vIPI,'#0.00'),',','.')"/>
						</vIPI>
						<vPIS>
							<xsl:value-of select="translate(format-number(enviNFe/NFe/infNFe/total/ICMSTot/vPIS,'#0.00'),',','.')"/>
						</vPIS>
						<vCOFINS>
							<xsl:value-of select="translate(format-number(enviNFe/NFe/infNFe/total/ICMSTot/vCOFINS,'#0.00'),',','.')"/>
						</vCOFINS>
						<vOutro>
							<xsl:value-of select="translate(format-number(enviNFe/NFe/infNFe/total/ICMSTot/vOutro,'#0.00'),',','.')"/>
						</vOutro>
						<vNF>
							<xsl:value-of select="translate(format-number(enviNFe/NFe/infNFe/total/ICMSTot/vNF,'#0.00'),',','.')"/>
						</vNF>

            
          </ICMSTot>
				</total>
				<transp>
					<modFrete>
						<xsl:value-of select="enviNFe/NFe/infNFe/transp/modFrete"/>
					</modFrete>
					<transporta>
						<xsl:if test="enviNFe/NFe/infNFe/transp/transporta/CNPJ != ''">
							<CNPJ>
								<xsl:value-of select="enviNFe/NFe/infNFe/transp/transporta/CNPJ"/>
							</CNPJ>
						</xsl:if>
						<xsl:if test="enviNFe/NFe/infNFe/transp/transporta/CPF != ''">
							<CPF>
								<xsl:value-of select="translate(enviNFe/NFe/infNFe/transp/transporta/CPF,'-','')"/>
							</CPF>
						</xsl:if>
						<xNome>
							<xsl:value-of select="enviNFe/NFe/infNFe/transp/transporta/xNome"/>
						</xNome>
            <xsl:if test="enviNFe/NFe/infNFe/transp/transporta/IE != ''">
              <IE>
                <xsl:value-of select="enviNFe/NFe/infNFe/transp/transporta/IE"/>
              </IE>
            </xsl:if>
            <xsl:if test="enviNFe/NFe/infNFe/transp/transporta/xEnder != ''">
              <xEnder>
                <xsl:value-of select="enviNFe/NFe/infNFe/transp/transporta/xEnder"/>
              </xEnder>
            </xsl:if>
            <xsl:if test="enviNFe/NFe/infNFe/transp/transporta/xMun != ''">
              <xMun>
                <xsl:value-of select="enviNFe/NFe/infNFe/transp/transporta/xMun"/>
              </xMun>
            </xsl:if >
						<UF>
							<xsl:value-of select="enviNFe/NFe/infNFe/transp/transporta/UF"/>
						</UF>
					</transporta>
          <xsl:if test="number(enviNFe/NFe/infNFe/transp/retTransp/vBCRet) != 0">
            <retTransp>
              <vServ>
                <xsl:value-of select="enviNFe/NFe/infNFe/transp/retTransp/vServ"/>
              </vServ>
              <vBCRet>
                <xsl:value-of select="enviNFe/NFe/infNFe/transp/retTransp/vBCRet"/>
              </vBCRet>
              <pICMSRet>
                <xsl:value-of select="enviNFe/NFe/infNFe/transp/retTransp/pICMSRet"/>
              </pICMSRet>
              <vICMSRet>
                <xsl:value-of select="enviNFe/NFe/infNFe/transp/retTransp/vICMSRet"/>
              </vICMSRet>
              <CFOP>
                <xsl:value-of select="enviNFe/NFe/infNFe/transp/retTransp/CFOP"/>
              </CFOP>
              <cMunFG>
                <xsl:value-of select="enviNFe/NFe/infNFe/transp/retTransp/cMunFG"/>
              </cMunFG>
            </retTransp>
          </xsl:if>
					<veicTransp>
						<placa>
							<xsl:value-of select="translate(enviNFe/NFe/infNFe/transp/veicTransp/placa, ' ', '')"/>
						</placa>
						<UF>
							<xsl:value-of select="enviNFe/NFe/infNFe/transp/veicTransp/UF"/>
						</UF>
            <xsl:if test="enviNFe/NFe/infNFe/transp/veicTransp/RNTC != ''">
              <RNTC>
                <xsl:value-of select="enviNFe/NFe/infNFe/transp/veicTransp/RNTC"/>
              </RNTC>
            </xsl:if>
					</veicTransp>
          <xsl:if test="enviNFe/NFe/infNFe/transp/reboque/placa != ''">
            <reboque>
              <placa>
                <xsl:value-of select="enviNFe/NFe/infNFe/transp/reboque/placa"/>
              </placa>
              <UF>
                <xsl:value-of select="enviNFe/NFe/infNFe/transp/reboque/UF"/>
              </UF>
              <RNTC>
                <xsl:value-of select="enviNFe/NFe/infNFe/transp/reboque/RNTC"/>
              </RNTC>
            </reboque>
          </xsl:if>
					<xsl:for-each select="enviNFe/NFe/infNFe/transp/vol">
						<vol>
              <xsl:if test="qVol != ''">
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
              <xsl:if test="pesoL != ''">
                <pesoL>
                  <xsl:value-of select="pesoL"/>
                </pesoL>
              </xsl:if>
              <xsl:if test="pesoB != ''">
                <pesoB>
                  <xsl:value-of select="pesoB"/>
                </pesoB>
              </xsl:if>
              <xsl:if test="lacres/nLacre != ''">
                <lacres>
                  <nLacre>
                    <xsl:value-of select="lacres/nLacre"/>
                  </nLacre>
                </lacres>
              </xsl:if>
						</vol>
					</xsl:for-each>
				</transp>
				<cobr>
          <xsl:if test="enviNFe/NFe/infNFe/cobr/fat/nFat != ''">
            <fat>
              <nFat>
                <xsl:value-of select="enviNFe/NFe/infNFe/cobr/fat/nFat"/>
              </nFat>
              <xsl:if test="enviNFe/NFe/infNFe/cobr/fat/vOrig != 0">
                <vOrig>
                  <xsl:value-of select="translate(format-number(enviNFe/NFe/infNFe/cobr/fat/vOrig,'#0.00'),',','.')"/>
                </vOrig>
              </xsl:if>
              <xsl:if test="enviNFe/NFe/infNFe/cobr/fat/vDesc != 0">
                <vDesc>
                  <xsl:value-of select="translate(format-number(enviNFe/NFe/infNFe/cobr/fat/vDesc,'#0.00'),',','.')"/>
                </vDesc>
              </xsl:if>
              <vLiq>
                <xsl:value-of select="translate(format-number(enviNFe/NFe/infNFe/cobr/fat/vLiq,'#0.00'),',','.')"/>
              </vLiq>
            </fat>
          </xsl:if>
					<xsl:for-each select="enviNFe/NFe/infNFe/cobr/dup">
						<xsl:if test="nDup != ''"> 
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
        <xsl:if test="enviNFe/NFe/infNFe/infAdic/infAdFisco != '' or enviNFe/NFe/infNFe/infAdic/infCpl != ''">
          <infAdic>
            <xsl:if test="enviNFe/NFe/infNFe/infAdic/infAdFisco != ''">
              <infAdFisco>
                <xsl:value-of select="enviNFe/NFe/infNFe/infAdic/infAdFisco"/>
              </infAdFisco>
            </xsl:if>
            <xsl:if test="enviNFe/NFe/infNFe/infAdic/infCpl != ''">
              <infCpl>
                <xsl:value-of select="enviNFe/NFe/infNFe/infAdic/infCpl"/>
              </infCpl>
            </xsl:if>
          </infAdic>
        </xsl:if>
			</infNFe>
		</NFe>
	</xsl:template>
</xsl:stylesheet>