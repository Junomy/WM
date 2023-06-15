export class LineModel {
    id: number;
    name: string;
    items: LineValue[];
}

export class LineValue {
    amount: number;
    sellDate: Date;
}