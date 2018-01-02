<?php 
namespace Ciente\Model\VO;

class Anexos
{
const TABLE_NAME = "anexos"; 
const KEY_ID = "id"; //integer
const ID_SOLICITACAO = "idSolicitacao"; //integer
const ID_ATENDIMENTO = "idAtendimento"; //integer
const ID_COMUNICACAO_INTERNA = "idComunicacaoInterna"; //integer
const CAMINHO = "caminho"; //character varying
const DATA = "data"; //timestamp with time zone
const TABLE_FIELDS = [self::ID_SOLICITACAO, self::ID_ATENDIMENTO, self::ID_COMUNICACAO_INTERNA, self::CAMINHO, self::DATA]; 
public  $id; 
public  $idSolicitacao; 
public  $idAtendimento; 
public  $idComunicacaoInterna; 
public  $caminho; 
public  $data; 

public  function getId ()
{
return $this->id;
}

public  function setId ( $id)
{
$this->id = $id;
}

public  function getIdSolicitacao ()
{
return $this->idsolicitacao;
}

public  function setIdSolicitacao ( $idsolicitacao)
{
$this->id = $idsolicitacao;
}

public  function getIdAtendimento ()
{
return $this->idatendimento;
}

public  function setIdAtendimento ( $idatendimento)
{
$this->id = $idatendimento;
}

public  function getIdComunicacaoInterna ()
{
return $this->idcomunicacaointerna;
}

public  function setIdComunicacaoInterna ( $idcomunicacaointerna)
{
$this->id = $idcomunicacaointerna;
}

public  function getCaminho ()
{
return $this->caminho;
}

public  function setCaminho ( $caminho)
{
$this->id = $caminho;
}

public  function getData ()
{
return $this->data;
}

public  function setData ( $data)
{
$this->id = $data;
}

}