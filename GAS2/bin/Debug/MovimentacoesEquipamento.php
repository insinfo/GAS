<?php 
namespace Ciente\Model\VO;
class MovimentacoesEquipamento
{
const TABLE_NAME = "movimentacoes_equipamento"; 
const KEY_ID = "id"; \\integer
const ID_EQUIPAMENTO = "idEquipamento"; \\integer
const DATA = "data"; \\timestamp with time zone
const TIPO = "tipo"; \\smallint
const MOTIVO = "motivo"; \\smallint
const OBSERVACAO = "observacao"; \\text
const TABLE_FIELDS = [self::ID_EQUIPAMENTO, self::DATA, self::TIPO, self::MOTIVO, self::OBSERVACAO]; 
public  $id; 
public  $idEquipamento; 
public  $data; 
public  $tipo; 
public  $motivo; 
public  $observacao; 

public  function getId ()
{
return $this->id;
}

public  function setId ( $id)
{
$this->id = $id;
}

public  function getIdEquipamento ()
{
return $this->idequipamento;
}

public  function setIdEquipamento ( $idequipamento)
{
$this->id = $idequipamento;
}

public  function getData ()
{
return $this->data;
}

public  function setData ( $data)
{
$this->id = $data;
}

public  function getTipo ()
{
return $this->tipo;
}

public  function setTipo ( $tipo)
{
$this->id = $tipo;
}

public  function getMotivo ()
{
return $this->motivo;
}

public  function setMotivo ( $motivo)
{
$this->id = $motivo;
}

public  function getObservacao ()
{
return $this->observacao;
}

public  function setObservacao ( $observacao)
{
$this->id = $observacao;
}

}