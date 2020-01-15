import { Component, OnInit,EventEmitter, Output } from '@angular/core';
import { HttpService} from './http.service';
import { HttpEventType} from '@angular/common/http';


@Component({
  selector: 'app-image-upload-with-preview',
  templateUrl: './app.image-upload-with-preview.component.html',
  styleUrls: ['./app.image-upload-with-preview.component.css'],
  providers:[HttpService]
})

export class ImageUploadWithPreviewComponent implements OnInit {

  @Output() SubmitDataEvent = new EventEmitter();
  fileData: File = null;
  previewUrl: any = null;
  fileUploadProgress: string = null;
  uploadedFilePath: string = null; 
  validationError: string=null;


  constructor(private httpService: HttpService) { }


  ngOnInit() {
  }

  fileProgress(fileInput: any) {
    this.fileData = <File>fileInput.target.files[0];
    this.preview();
  }

  preview() {
    // Show preview 
    

    var mimeType = this.fileData.type;
    if (mimeType.match(/image\/*/) != null) {

    var reader = new FileReader();
        reader.readAsDataURL(this.fileData);
        reader.onload = (_event) => {
          this.previewUrl = reader.result;
        }     
    }
    
    if(this.validationError!=null)
    this.validateFile(this.fileData.name,this.fileData.size);     

  }


  onSubmit() {    

    if(this.validationError!=null)
       return;

    this.fileUploadProgress = '0%';

     this.httpService.PostFileData(this.fileData)
    .subscribe(events => {
              if (events.type === HttpEventType.UploadProgress) {
                this.fileUploadProgress = Math.round(events.loaded / events.total * 100) + '%';
                console.log(this.fileUploadProgress);
              } else if (events.type === HttpEventType.Response) {
                this.fileUploadProgress = '100%';              
                console.log(events.body);
                alert('Data was uploaded successfully !!');
                this.SubmitDataEvent.emit();
              }    
            }, error=>this.validationError= error.message)  
  }

  validateFile(name: String, size:Number)
   {   
    var ext = name.substring(name.lastIndexOf('.') + 1);
    var fileTypeItems =this.httpService.GetRestrictedFileTypes()
   
    fileTypeItems.forEach(element => {
      element.forEach(item=> {          
    
          if(item.Name==ext.toLowerCase())
          {      
            var extError='File with extention '+ ext+' not allowed.'; 
            this.validationError=extError;      
          }
          if(item.Size < size)
          {        
            var sizeError=' File size only '+ item.Size+' bytes allowed.'; 
            this.validationError=this.validationError.concat(sizeError);     
          } 
        })    
      }); 
}
 
}



