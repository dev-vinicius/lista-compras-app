export class Item {
    id: number;
    list_id: number;
    description: string;
    note: string;
    quantity: number;
    price: number;
    active: boolean;
    created_at: Date;
    updated_at: Date;

    total(): number {
        return this.quantity * this.price
    }
}