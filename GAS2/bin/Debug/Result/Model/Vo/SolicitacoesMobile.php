<?php 
namespace Ciente\Model\VO;

class SolicitacoesMobile
{
const TABLE_NAME = "solicitacoes_mobile"; 
const KEY_ID = "idSolicitacao"; //integer
const TOKEN_MOBILE = "tokenMobile"; //text
const TABLE_FIELDS = [self::TOKEN_MOBILE]; 
public  $idSolicitacao; 
public  $tokenMobile; 

public  function getIdSolicitacao ()
{
return $this->idsolicitacao;
}

public  function setIdSolicitacao ( $idsolicitacao)
{
$this->id = $idsolicitacao;
}

public  function getTokenMobile ()
{
return $this->tokenmobile;
}

public  function setTokenMobile ( $tokenmobile)
{
$this->id = $tokenmobile;
}

}