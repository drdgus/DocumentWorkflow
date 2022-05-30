import {RequiredElements} from "./requiredElements";

export interface TemplateField
{
  name: string;
  nameForUser: string;
  visibleForUser: boolean;
  type: string;
  value: string;
  order: number;
  isDisabled: boolean;
  requiredElements: RequiredElements[]
}

