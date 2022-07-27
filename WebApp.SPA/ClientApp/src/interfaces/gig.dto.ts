export class GigDto {
    id: string;
    text: string;
    title: string;
    
    constructor(id: string, text: string, title: string){
        this.id = id;
        this.text = text;
        this.title = title;
    }
}