import {LogBook} from "./logBook";
import {TemplateField} from "./templateField";
import {RequiredModule} from "./requiredModule";

export interface Category {
  id:number;
  parentId: number;
  name: string;
  customTemplateFileName: string;
  documentTypeId: number;
  logBookId: number;
  documentType: DocumentType;
  logBook: LogBook;
  fields: TemplateField[];
  requiredModule: RequiredModule;
}

