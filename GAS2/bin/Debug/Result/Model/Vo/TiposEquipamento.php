<?php 
namespace Ciente\Model\VO;

class TiposEquipamento
{
const TABLE_NAME = "tipos_equipamento"; 
const KEY_ID = "id"; //integer
const ID_SETOR = "idSetor"; //integer
const NOME = "nome"; //character varying
const DESCRICAO = "descricao"; //text
const ATIVO = "ativo"; //boolean
const TABLE_FIELDS = [self::ID_SETOR, self::NOME, self::DESCRICAO, self::ATIVO]; 
public  $id; 
public  $idSetor; 
public  $nome; 
public  $descricao; 
public  $ativo; 

public  function getId ()
{
return $this->id;
}

public  function setId ( $id)
{
$this->id = $id;
}

public  function getIdSetor ()
{
return $this->idsetor;
}

public  function setIdSetor ( $idsetor)
{
$this->id = $idsetor;
}

public  function getNome ()
{
return $this->nome;
}

public  function setNome ( $nome)
{
$this->id = $nome;
}

public  function getDescricao ()
{
return $this->descricao;
}

public  function setDescricao ( $descricao)
{
$this->id = $descricao;
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