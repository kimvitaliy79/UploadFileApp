import {Injectable} from '@angular/core';
import {HttpClient, HttpEventType} from '@angular/common/http';
import {Observable} from 'rxjs';
import{FileItem} from "./fileItem";
import{FileTypeItem} from "./fileTypeItem"; 
import { map } from 'rxjs/operators';

  
@Injectable({
    providedIn: 'root'
})
export class HttpService{
  
    constructor(private http: HttpClient){ }

    getPostData1() {
        return this.http.get<FileTypeItem[]>('http://localhost:44306/api/File/GetFileValidation');
    }

    GetRestrictedFileTypes():Observable<FileTypeItem[]> {

        return this.http.get('http://localhost:44306/api/File/GetFileValidation').pipe(
            map(
                data=>{
                    let fileValidList= data["RestrictFileData"];
                    return fileValidList.map(function(file:any) {                  
                        return { Name: file.Name, Size: file.Size }
                    });
                }
            ))
    }
      
    GetFileItems():Observable<FileItem[]> {

        return this.http.get('http://localhost:44306/api/File/GetFileList').pipe(
            map(
                data=>{
                    let fileList= data["Items"];
                    return fileList.map(function(file:any) {
                        return { Name: file.Name, Size:file.Size, Type:file.Type, UploadDate:file.UploadDate }
                    });
                }
            ))
    }
    
    PostFileData (fileData:any)
    {

        const formData = new FormData();
        formData.append('files', fileData);
    
       return this.http.post('http://localhost:44306/api/File/UploadFile', formData, {
          reportProgress: true,
          observe: 'events'
        })      
    }
}
        
    
