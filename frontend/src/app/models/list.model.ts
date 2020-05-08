import { Item } from './item.model';
export class List {
    id: number;
    user_id: number;
    title: string;
    subtitle: string;
    active: boolean;
    created_at: Date;
    updated_at: Date;
    itens: Item[];
}