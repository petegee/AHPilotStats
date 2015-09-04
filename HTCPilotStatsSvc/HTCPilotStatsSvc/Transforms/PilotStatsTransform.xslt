<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output method="xml" version="1.0" encoding="UTF-8" indent="yes"/>
  <xsl:template match="/">
    <AcesHighPilotStats xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" version="2.0">
      <GameId></GameId>
      <TourId></TourId>
      <TourDetails></TourDetails>
      <TourType></TourType>
      <VsObjects>
        <xsl:for-each select="/html/body/table[3]/tr">
          <xsl:variable name="itr" select="position()" />
          <xsl:variable name="max" select="last()-1" />
          <xsl:choose>
            <xsl:when test ="$itr&lt;'3'" />
            <xsl:when test ="$itr&gt;$max" />
            <xsl:otherwise>
              <ObjectScore>
                <Model>
                  <xsl:value-of select='./td[1]'/>
                </Model>
                <KillsIn>
                  <xsl:value-of select='./td[2]'/>
                </KillsIn>
                <KillsOf>
                  <xsl:value-of select='./td[3]'/>
                </KillsOf>
                <KilledBy>
                  <xsl:value-of select='./td[4]' />
                </KilledBy>
                <DiedIn>
                  <xsl:value-of select='./td[5]' />
                </DiedIn>
              </ObjectScore>
            </xsl:otherwise>
          </xsl:choose>
        </xsl:for-each>
      </VsObjects>
      <VsCountry>
        <!-- Country scores no longer sourced. -->
        <CountryScore>
          <Country>Bishop</Country>
          <KillsAs>0</KillsAs>
          <KillsOf>0</KillsOf>
          <KilledBy>0</KilledBy>
        </CountryScore>
        <CountryScore>
          <Country>Knight</Country>
          <KillsAs>0</KillsAs>
          <KillsOf>0</KillsOf>
          <KilledBy>0</KilledBy>
        </CountryScore>
        <CountryScore>
          <Country>Rook</Country>
          <KillsAs>0</KillsAs>
          <KillsOf>0</KillsOf>
          <KilledBy>0</KilledBy>
        </CountryScore>
      </VsCountry>
    </AcesHighPilotStats>
  </xsl:template>
</xsl:stylesheet>
