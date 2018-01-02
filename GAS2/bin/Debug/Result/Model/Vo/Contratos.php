<?php 
namespace Ciente\Model\VO;

class Contratos
{
const TABLE_NAME = "contratos"; 
const KEY_ID = "id"; //integer
const ID_PESSOA_JURIDICA = "idPessoaJuridica"; //integer
const DESCRICAO = "descricao"; //text
const OBSERVACAO = "observacao"; //text
const ATIVO = "ativo"; //boolean
const TABLE_FIELDS = [self::ID_PESSOA_JURIDICA, self::DESCRICAO, self::OBSERVACAO, self::ATIVO]; 
public  $id; 
public  $idPessoaJuridica; 
public  $descricao; 
public  $observacao; 
public  $ativo; 

public  function getId ()
{
return $this->id;
}

public  function setId ( $id)
{
$this->id = $id;
}

public  function getIdPessoaJuridica ()
{
return $this->idpessoajuridica;
}

public  function setIdPessoaJuridica ( $idpessoajuridica)
{
$this->id = $idpessoajuridica;
}

public  function getDescricao ()
{
return $this->descricao;
}

public  function setDescricao ( $descricao)
{
$this->id = $descricao;
}

public  function getObservacao ()
{
return $this->observacao;
}

public  function setObservacao ( $observacao)
{
$this->id = $observacao;
}

public  function getAtivo ()
{
return $this->ativo;
}

public  function setAtivo ( $ativo)
{
$this->id = $ativo;
}

}