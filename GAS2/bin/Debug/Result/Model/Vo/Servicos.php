<?php 
namespace Ciente\Model\VO;

class Servicos
{
const TABLE_NAME = "servicos"; 
const KEY_ID = "id"; //integer
const ID_TIPO_SERVICO = "idTipoServico"; //integer
const ID_CONTRATO = "idContrato"; //integer
const CODIGO_REFERENCIA = "codigoReferencia"; //integer
const NOME = "nome"; //character varying
const DESCRICAO = "descricao"; //text
const PRAZO_BAIXA = "prazoBaixa"; //integer
const PRAZO_NORMAL = "prazoNormal"; //integer
const PRAZO_ALTA = "prazoAlta"; //integer
const PRAZO_URGENTE = "prazoUrgente"; //integer
const ATIVO = "ativo"; //boolean
const TABLE_FIELDS = [self::ID_TIPO_SERVICO, self::ID_CONTRATO, self::CODIGO_REFERENCIA, self::NOME, self::DESCRICAO, self::PRAZO_BAIXA, self::PRAZO_NORMAL, self::PRAZO_ALTA, self::PRAZO_URGENTE, self::ATIVO]; 
public  $id; 
public  $idTipoServico; 
public  $idContrato; 
public  $codigoReferencia; 
public  $nome; 
public  $descricao; 
public  $prazoBaixa; 
public  $prazoNormal; 
public  $prazoAlta; 
public  $prazoUrgente; 
public  $ativo; 

public  function getId ()
{
return $this->id;
}

public  function setId ( $id)
{
$this->id = $id;
}

public  function getIdTipoServico ()
{
return $this->idtiposervico;
}

public  function setIdTipoServico ( $idtiposervico)
{
$this->id = $idtiposervico;
}

public  function getIdContrato ()
{
return $this->idcontrato;
}

public  function setIdContrato ( $idcontrato)
{
$this->id = $idcontrato;
}

public  function getCodigoReferencia ()
{
return $this->codigoreferencia;
}

public  function setCodigoReferencia ( $codigoreferencia)
{
$this->id = $codigoreferencia;
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

public  function getPrazoBaixa ()
{
return $this->prazobaixa;
}

public  function setPrazoBaixa ( $prazobaixa)
{
$this->id = $prazobaixa;
}

public  function getPrazoNormal ()
{
return $this->prazonormal;
}

public  function setPrazoNormal ( $prazonormal)
{
$this->id = $prazonormal;
}

public  function getPrazoAlta ()
{
return $this->prazoalta;
}

public  function setPrazoAlta ( $prazoalta)
{
$this->id = $prazoalta;
}

public  function getPrazoUrgente ()
{
return $this->prazourgente;
}

public  function setPrazoUrgente ( $prazourgente)
{
$this->id = $prazourgente;
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