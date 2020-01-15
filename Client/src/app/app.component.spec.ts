import { TestBed, async, inject } from '@angular/core/testing';
import { HttpService} from './http.service' ;
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { HttpEventType} from '@angular/common/http';

describe('HttpService', () => {
  let service: HttpService;
  let http: HttpTestingController;
    beforeEach(() =>{ TestBed.configureTestingModule({   
    imports: [HttpClientTestingModule],
    providers: [HttpService]      
    });
    service = TestBed.get(HttpService);   
    http = TestBed.get(HttpTestingController);
  })
      
  it('Created service', () => {     
    expect(service).toBeTruthy();  }); 
  
    it('GetFileItems',
    inject([HttpService], async (myService: HttpService) => {
        const result = await myService.GetFileItems()
        expect(result).not.toBeUndefined()      
     }))

     it('GetRestrictedFileTypes',
     inject([HttpService], async (myService: HttpService) => {
         const result = await myService.GetRestrictedFileTypes()
         expect(result).not.toBeNull()      
      }))

      it('PostFileData',
      inject([HttpService], async (myService: HttpService) => {
        var file = new File(['355544'], 'test-file.jpg', { type: 'image/jpeg'});
          const result = await myService.PostFileData(file)
          expect(result).not.toBeNull()      
       }))


  });


 


