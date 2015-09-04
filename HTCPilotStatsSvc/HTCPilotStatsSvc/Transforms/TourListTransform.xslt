<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="2.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
	<xsl:output method="xml" version="1.0" encoding="UTF-8" indent="yes"/> 
	<xsl:template match="/">
		<AHTourList>
			<xsl:for-each select="/html/body/table/tr/td/form/select/option">
				<AHTourNode>
					 <TourNumber><xsl:value-of select='substring-after(@value, "Tour")'/></TourNumber>
					 <xsl:choose>
             <xsl:when test='starts-with(@value, "Tour")'>
               <TourType>MainArenaTour</TourType>
             </xsl:when>
             <xsl:when test='starts-with(@value, "LW")'>
               <TourType>MainArenaTour</TourType>
             </xsl:when>
             <xsl:when test='starts-with(@value, "EW")'>
               <TourType>EWMainArenaTour</TourType>
             </xsl:when>
             <xsl:when test='starts-with(@value, "MW")'>
               <TourType>MWMainArenaTour</TourType>
             </xsl:when>
             <xsl:when test='starts-with(@value, "WW1")'>
               <TourType>WW1Tour</TourType>
             </xsl:when>
             <xsl:when test='starts-with(@value, "CtTour")'>
               <TourType>AvATour</TourType>
             </xsl:when>
						 <xsl:otherwise>
							 <TourType>Unknown</TourType>
						 </xsl:otherwise>
					</xsl:choose>
					<TourStartDate>
						<xsl:value-of select='substring(., string-length(substring-before(., " to"))-10, 11)'/>
					</TourStartDate>
					<TourEndDate>
            <xsl:value-of select='substring-after(., "to ")'/>
          </TourEndDate>
          <TourSelectArg>
            <xsl:value-of select='@value' />
          </TourSelectArg>
				</AHTourNode>
			</xsl:for-each>
		</AHTourList>
	</xsl:template>
</xsl:stylesheet>
