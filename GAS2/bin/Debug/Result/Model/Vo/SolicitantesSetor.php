<?php 
namespace Ciente\Model\VO;

class SolicitantesSetor
{
const TABLE_NAME = "solicitantes_setor"; 
const KEY_ID = "idPessoa"; //integer
const ID_SETOR = "idSetor"; //integer
const TABLE_FIELDS = [self::ID_SETOR]; 
public  $idPessoa; 
public  $idSetor; 

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

}