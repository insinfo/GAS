using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAS
{
    public class TemplatesPHP
    {
        public string DefaultErrorMessage = "Ouve um erro desconhecido.";

        public string GetAll(ClassPHP model)
        {
            var properties = model.GetAllProperties(AccessTypePHP.Const);
            var filter = Environment.NewLine + "     ->where(function($query) use($request, $search){" + Environment.NewLine;
            foreach (string prop in properties)
            {
                if (prop != "TABLE_NAME" && prop != "TABLE_FIELDS")
                {
                    filter += "     $query->orWhere(" + model.Name + "::" + prop + ", DBLayer::OPERATOR_ILIKE, $search);" + Environment.NewLine;
                }
            }
            filter += "  });" + Environment.NewLine;
            var template = @"
            try
            {
            $parametros = $request->getParsedBody();               
            $draw = $parametros['draw'];
            $limit = $parametros['length'];
            $offset = $parametros['start'];
            $search = '%'.$parametros['search'].'%';
            $query = DBLayer::Connect()->table(" + model.Name + "::TABLE_NAME)" + filter +
            @"$totalRecords = $query->count();
            $data = $query->orderBy(" + model.Name + @"::KEY_ID, 'asc')
            ->take($limit)->offset($offset)->get();
            $result['draw'] = $draw;
            $result['recordsFiltered'] = $totalRecords;
            $result['recordsTotal'] = $totalRecords;
            $result['data'] = $data;
            }
            catch (Exception $e)
            {
                    return $response->withStatus(400)->withJson((['message' => '" + DefaultErrorMessage + @"', 'exception' => $e->getMessage(), 'line' => $e->getLine(), 'file' => $e->getFile()]));
            }
            return $response->withStatus(200)->withJson($result); ";

            return template;
        }

        public string Save(ClassPHP model)
        {
            var template = @"
            try
            {
                $id"+ model.Name + " = $request->getAttribute('id" + model.Name + @"');
                $formData = Utils::filterArrayByArray($request->getParsedBody(), " + model.Name + @"::TABLE_FIELDS);

                if ($id" + model.Name + @")
                {
                    DBLayer::Connect()->table(" + model.Name + @"::TABLE_NAME)->update($formData);
                }
                else
                {
                    DBLayer::Connect()->table(" + model.Name + @"::TABLE_NAME)->insertGetId($formData);
                }
            }
            catch (Exception $e)
            {
                return $response->withStatus(400)->withJson((['message' => '" + DefaultErrorMessage + @"', 'exception' => $e->getMessage(), 'line' => $e->getLine(), 'file' => $e->getFile()]));
            }
            return $response->withStatus(200)->withJson((['message' => 'Salvo com sucesso']));";
            return template;
        }

        public string Get(ClassPHP model)
        {
            var template = @" 
            try
            {
                $id" + model.Name + " = $request->getAttribute('" + model.Name + @"');
                $" + Util.LowerCaseFirst(model.Name) + " = DBLayer::Connect()->table(" + model.Name + @"::TABLE_NAME)
                    ->where(" + model.Name + @"::KEY_ID,DBLayer::OPERATOR_EQUAL,$id" + model.Name + @")->first();
                return $response->withStatus(200)->withJson($" + Util.LowerCaseFirst(model.Name) + @");
            }
            catch (Exception $e)
            {
                return $response->withStatus(400)->withJson(['message' => '" + DefaultErrorMessage + @"', 'exception' => $e->getMessage(), 'line' => $e->getLine(), 'file' => $e->getFile()]);
            }";

            return template;
        }

        public string Delete(ClassPHP model)
        {
            var template = @"
            try
            {
                $formData = $request->getParsedBody();
                $ids = $formData['ids'];
                $idsCount = count($ids);
                $itensDeletadosCount = 0;
                foreach ($ids as $id)
                {
                    $" + Util.LowerCaseFirst(model.Name) + @" = DBLayer::Connect()->table(" + model.Name + @"::TABLE_NAME)
                        ->where(" + model.Name + @"::KEY_ID,DBLayer::OPERATOR_EQUAL,$id)->first();

                    if ($" + Util.LowerCaseFirst(model.Name) + @")
                    {
                        if (DBLayer::Connect()->table(" + model.Name + @"::TABLE_NAME)->delete($id))
                        {
                            $itensDeletadosCount++;
                        }
                    }
                }
                if ($itensDeletadosCount == $idsCount)
                {
                    return $response->withStatus(200)->withJson(['message' => 'Todos os itens foram deletados com sucesso']);
                }
                else if ($itensDeletadosCount > 0)
                {
                    return $response->withStatus(200)->withJson(['message' => 'Nem todos os itens foram deletados com sucesso']);
                }
                else
                {
                    return $response->withStatus(200)->withJson((['message' => 'Nenhum dos itens foram deletados']));
                }
            }
            catch (Exception $e)
            {
                return $response->withStatus(400)->withJson(['message' => '" + DefaultErrorMessage + @"', 'exception' => $e->getMessage(), 'line' => $e->getLine(), 'file' => $e->getFile()]);
            }";

            return template;
        }

    }
}
