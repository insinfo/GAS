<?php 
namespace Ciente\Model\VO;

class ModelosEquipamento
{
const TABLE_NAME = "modelos_equipamento"; 
const KEY_ID = "id"; //integer
const ID_TIPO_EQUIPAMENTO = "idTipoEquipamento"; //integer
const DESCRICAO = "descricao"; //text
const ATIVO = "ativo"; //boolean
const TABLE_FIELDS = [self::ID_TIPO_EQUIPAMENTO, self::DESCRICAO, self::ATIVO]; 
public  $id; 
public  $idTipoEquipamento; 
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

public  function getIdTipoEquipamento ()
{
return $this->idtipoequipamento;
}

public  function setIdTipoEquipamento ( $idtipoequipamento)
{
$this->id = $idtipoequipamento;
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