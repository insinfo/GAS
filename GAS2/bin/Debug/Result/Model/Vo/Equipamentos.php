<?php 
namespace Ciente\Model\VO;

class Equipamentos
{
const TABLE_NAME = "equipamentos"; 
const KEY_ID = "id"; //integer
const ID_MODELO_EQUIPAMENTO = "idModeloEquipamento"; //integer
const ID_CONTRATO = "idContrato"; //integer
const PATRIMONIO_PMRO = "patrimonioPmro"; //character varying
const PATRIMONIO_INTERNO = "patrimonioInterno"; //character varying
const PATRIMONIO_EMPRESA = "patrimonioEmpresa"; //character varying
const OBSERVACAO = "observacao"; //text
const SITUACAO = "situacao"; //smallint
const STATUS = "status"; //smallint
const TABLE_FIELDS = [self::ID_MODELO_EQUIPAMENTO, self::ID_CONTRATO, self::PATRIMONIO_PMRO, self::PATRIMONIO_INTERNO, self::PATRIMONIO_EMPRESA, self::OBSERVACAO, self::SITUACAO, self::STATUS]; 
public  $id; 
public  $idModeloEquipamento; 
public  $idContrato; 
public  $patrimonioPmro; 
public  $patrimonioInterno; 
public  $patrimonioEmpresa; 
public  $observacao; 
public  $situacao; 
public  $status; 

public  function getId ()
{
return $this->id;
}

public  function setId ( $id)
{
$this->id = $id;
}

public  function getIdModeloEquipamento ()
{
return $this->idmodeloequipamento;
}

public  function setIdModeloEquipamento ( $idmodeloequipamento)
{
$this->id = $idmodeloequipamento;
}

public  function getIdContrato ()
{
return $this->idcontrato;
}

public  function setIdContrato ( $idcontrato)
{
$this->id = $idcontrato;
}

public  function getPatrimonioPmro ()
{
return $this->patrimoniopmro;
}

public  function setPatrimonioPmro ( $patrimoniopmro)
{
$this->id = $patrimoniopmro;
}

public  function getPatrimonioInterno ()
{
return $this->patrimoniointerno;
}

public  function setPatrimonioInterno ( $patrimoniointerno)
{
$this->id = $patrimoniointerno;
}

public  function getPatrimonioEmpresa ()
{
return $this->patrimonioempresa;
}

public  function setPatrimonioEmpresa ( $patrimonioempresa)
{
$this->id = $patrimonioempresa;
}

public  function getObservacao ()
{
return $this->observacao;
}

public  function setObservacao ( $observacao)
{
$this->id = $observacao;
}

public  function getSituacao ()
{
return $this->situacao;
}

public  function setSituacao ( $situacao)
{
$this->id = $situacao;
}

public  function getStatus ()
{
return $this->status;
}

public  function setStatus ( $status)
{
$this->id = $status;
}

}