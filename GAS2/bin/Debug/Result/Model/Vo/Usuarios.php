<?php 
namespace Ciente\Model\VO;

class Usuarios
{
const TABLE_NAME = "usuarios"; 
const KEY_ID = "idPessoa"; //integer
const ID_SETOR = "idSetor"; //integer
const LOGIN = "login"; //character varying
const PERFIL = "perfil"; //smallint
const ATIVO = "ativo"; //boolean
const TABLE_FIELDS = [self::ID_SETOR, self::LOGIN, self::PERFIL, self::ATIVO]; 
public  $idPessoa; 
public  $idSetor; 
public  $login; 
public  $perfil; 
public  $ativo; 

public  function getIdPessoa ()
{
return $this->idpessoa;
}

public  function setIdPessoa ( $idpessoa)
{
$this->id = $idpessoa;
}

public  function getIdSetor ()
{
return $this->idsetor;
}

public  function setIdSetor ( $idsetor)
{
$this->id = $idsetor;
}

public  function getLogin ()
{
return $this->login;
}

public  function setLogin ( $login)
{
$this->id = $login;
}

public  function getPerfil ()
{
return $this->perfil;
}

public  function setPerfil ( $perfil)
{
$this->id = $perfil;
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