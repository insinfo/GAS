<?php 
namespace Ciente\Controller;

use \Slim\Http\Request as Req;
use \Slim\Http\Response as Resp;
use \Exception;
use Ciente\Util\DBLayer;
use Ciente\Util\Utils;
use Ciente\Model\VO\Equipamentos;

class EquipamentosController
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
            $query = DBLayer::Connect()->table(Equipamentos::TABLE_NAME)
     ->where(function($query) use($request, $search){
     $query->orWhere(Equipamentos::KEY_ID, DBLayer::OPERATOR_ILIKE, $search);
     $query->orWhere(Equipamentos::ID_MODELO_EQUIPAMENTO, DBLayer::OPERATOR_ILIKE, $search);
     $query->orWhere(Equipamentos::ID_CONTRATO, DBLayer::OPERATOR_ILIKE, $search);
     $query->orWhere(Equipamentos::PATRIMONIO_PMRO, DBLayer::OPERATOR_ILIKE, $search);
     $query->orWhere(Equipamentos::PATRIMONIO_INTERNO, DBLayer::OPERATOR_ILIKE, $search);
     $query->orWhere(Equipamentos::PATRIMONIO_EMPRESA, DBLayer::OPERATOR_ILIKE, $search);
     $query->orWhere(Equipamentos::OBSERVACAO, DBLayer::OPERATOR_ILIKE, $search);
     $query->orWhere(Equipamentos::SITUACAO, DBLayer::OPERATOR_ILIKE, $search);
     $query->orWhere(Equipamentos::STATUS, DBLayer::OPERATOR_ILIKE, $search);
  });
$totalRecords = $query->count();
            $data = $query->orderBy(Equipamentos::KEY_ID, 'asc')
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
                $idEquipamentos = $request->getAttribute('idEquipamentos');
                $formData = Utils::filterArrayByArray($request->getParsedBody(), Equipamentos::TABLE_FIELDS);

                if ($idEquipamentos)
                {
                    DBLayer::Connect()->table(Equipamentos::TABLE_NAME)->update($formData);
                }
                else
                {
                    DBLayer::Connect()->table(Equipamentos::TABLE_NAME)->insertGetId($formData);
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
                $idEquipamentos = $request->getAttribute('Equipamentos');
                $equipamentos = DBLayer::Connect()->table(Equipamentos::TABLE_NAME)
                    ->where(Equipamentos::KEY_ID,DBLayer::OPERATOR_EQUAL,$idEquipamentos)->first();
                return $response->withStatus(200)->withJson($equipamentos);
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
                    $equipamentos = DBLayer::Connect()->table(Equipamentos::TABLE_NAME)
                        ->where(Equipamentos::KEY_ID,DBLayer::OPERATOR_EQUAL,$id)->first();

                    if ($equipamentos)
                    {
                        if (DBLayer::Connect()->table(Equipamentos::TABLE_NAME)->delete($id))
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