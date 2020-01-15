import { Component } from '@angular/core';
import { HttpService} from './http.service';
import{FileItem} from "./fileItem";


@Component({
  selector: 'app-list-files-component',
  templateUrl: './app.list-files.component.html',
  styleUrls: ['./app.list-files.component.css'],
  providers: [HttpService] 
})



export class ListFilesComponent {

  items: FileItem[]=[];
  erorrMsg: any =null;

  constructor(private httpService: HttpService) {      
   }
   
 
  SubmitData(){       
     this.ngOnInit();
  }
   
   ngOnInit(){   
    this.httpService.GetFileItems().subscribe(data => this.items=data, error=>this.erorrMsg=error.message );   
   }  

}



