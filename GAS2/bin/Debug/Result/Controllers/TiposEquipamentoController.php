<?php 
namespace Ciente\Controller;

use \Slim\Http\Request as Req;
use \Slim\Http\Response as Resp;
use \Exception;
use Ciente\Util\DBLayer;
use Ciente\Util\Utils;
use Ciente\Model\VO\TiposEquipamento;

class TiposEquipamentoController
{

public static function getAll (Req $request,Resp $response)
{

            try
            {
            $parametros = $request->getParsedBody();               
            $draw = $parametros['draw'];
            $limit = $parametros['length'];
            $offset = $parametros['start'];
            $search = '%'.$parametros['search'].'%';
            $query = DBLayer::Connect()->table(TiposEquipamento::TABLE_NAME)
     ->where(function($query) use($request, $search){
     $query->orWhere(TiposEquipamento::KEY_ID, DBLayer::OPERATOR_ILIKE, $search);
     $query->orWhere(TiposEquipamento::ID_SETOR, DBLayer::OPERATOR_ILIKE, $search);
     $query->orWhere(TiposEquipamento::NOME, DBLayer::OPERATOR_ILIKE, $search);
     $query->orWhere(TiposEquipamento::DESCRICAO, DBLayer::OPERATOR_ILIKE, $search);
     $query->orWhere(TiposEquipamento::ATIVO, DBLayer::OPERATOR_ILIKE, $search);
  });
$totalRecords = $query->count();
            $data = $query->orderBy(TiposEquipamento::KEY_ID, 'asc')
            ->take($limit)->offset($offset)->get();
            $result['draw'] = $draw;
            $result['recordsFiltered'] = $totalRecords;
            $result['recordsTotal'] = $totalRecords;
            $result['data'] = $data;
            }
            catch (Exception $e)
            {
                    return $response->withStatus(400)->withJson((['message' => 'Ouve um erro desconhecido.', 'exception' => $e->getMessage(), 'line' => $e->getLine(), 'file' => $e->getFile()]));
            }
            return $response->withStatus(200)->withJson($result); 
}

public static function save (Req $request,Resp $response)
{

            try
            {
                $idTiposEquipamento = $request->getAttribute('idTiposEquipamento');
                $formData = Utils::filterArrayByArray($request->getParsedBody(), TiposEquipamento::TABLE_FIELDS);

                if ($idTiposEquipamento)
                {
                    DBLayer::Connect()->table(TiposEquipamento::TABLE_NAME)->update($formData);
                }
                else
                {
                    DBLayer::Connect()->table(TiposEquipamento::TABLE_NAME)->insertGetId($formData);
                }
            }
            catch (Exception $e)
            {
                return $response->withStatus(400)->withJson((['message' => 'Ouve um erro desconhecido.', 'exception' => $e->getMessage(), 'line' => $e->getLine(), 'file' => $e->getFile()]));
            }
            return $response->withStatus(200)->withJson((['message' => 'Salvo com sucesso']));
}

public static function get (Req $request,Resp $response)
{
 
            try
            {
                $idTiposEquipamento = $request->getAttribute('TiposEquipamento');
                $tiposEquipamento = DBLayer::Connect()->table(TiposEquipamento::TABLE_NAME)
                    ->where(TiposEquipamento::KEY_ID,DBLayer::OPERATOR_EQUAL,$idTiposEquipamento)->first();
                return $response->withStatus(200)->withJson($tiposEquipamento);
            }
            catch (Exception $e)
            {
                return $response->withStatus(400)->withJson(['message' => 'Ouve um erro desconhecido.', 'exception' => $e->getMessage(), 'line' => $e->getLine(), 'file' => $e->getFile()]);
            }
}

public static function delete (Req $request,Resp $response)
{

            try
            {
                $formData = $request->getParsedBody();
                $ids = $formData['ids'];
                $idsCount = count($ids);
                $itensDeletadosCount = 0;
                foreach ($ids as $id)
                {
                    $tiposEquipamento = DBLayer::Connect()->table(TiposEquipamento::TABLE_NAME)
                        ->where(TiposEquipamento::KEY_ID,DBLayer::OPERATOR_EQUAL,$id)->first();

                    if ($tiposEquipamento)
                    {
                        if (DBLayer::Connect()->table(TiposEquipamento::TABLE_NAME)->delete($id))
                        {
                            $itensDeletadosCount++;
                        }
                    }
                }
                if ($itensDeletadosCount == $idsCount)
                {
                    return response()->json(['message' => 'Todos os itens foram deletados com sucesso']);
                }
                else if ($itensDeletadosCount > 0)
                {
                    return response()->json(['message' => 'Nem todos os itens foram deletados com sucesso']);
                }
                else
                {
                    return $response->withStatus(200)->withJson((['message' => 'Nenhum dos itens foram deletados']));
                }
            }
            catch (Exception $e)
            {
                return $response->withStatus(400)->withJson(['message' => 'Ouve um erro desconhecido.', 'exception' => $e->getMessage(), 'line' => $e->getLine(), 'file' => $e->getFile()]);
            }
}

}