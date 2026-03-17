import { Component, OnInit } from '@angular/core';
import { Taskmodel } from '../../shared/models/taskmodel';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Task } from '../../core/services/task';
import { CommonModule } from '@angular/common';
import { Auth } from '../../core/services/auth';
import { Subject, Subscriber } from 'rxjs';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.css',
})
export class Dashboard implements OnInit {
  userGuid: string = '';
  tasks: any;
  taskForm: FormGroup;
  isEditing = false;
  editingTaskGuid: string = '';
  constructor(
    private taskService: Task,
    private authService: Auth,
    private fb: FormBuilder
  ) {
    this.taskForm = this.fb.group({
      TaskId:[''],
      TaskTitle: ['', Validators.required],
      TaskDescription: ['', Validators.required],
      Priority: [1, [Validators.required, Validators.min(1), Validators.max(3)]]
    })
  }
  ngOnInit(): void {
    this.userGuid = this.authService.getUserGuid() || '';
    this.loadTasks();
  }
  loadTasks(): void {
    this.taskService.getAllTask({ UserGID: this.userGuid }).subscribe({
      next: (res: any) => {
        if (res.statuscode) {
          this.tasks = res.responselist;
        }
      },
      error: (err) => console.error('Failed To Load Tasks', err)
    });
  }
  addTask() {
    // this.taskService.addTask()
    if(this.taskForm.invalid){
      this.taskForm.markAllAsTouched();
      return;
    }
    const task: Taskmodel = {
      UserGID: this.userGuid,
      TaskTitle: this.taskForm.get('TaskTitle')?.value,
      TaskDescription: this.taskForm.get('TaskDescription')?.value,
      Priority: this.taskForm.get('Priority')?.value
    }
    this.taskService.addTask(task).subscribe({
      next: (res: any) => {
        if (res.statuscode == 101) {
          alert(res.message);
          // this.tasks = res.responselist;
          this.loadTasks();
          this.cancelEdit();
        }
      },
      error: (err) => console.error('Failed To Load Tasks', err)
    });
  }
  edittask(task: any) {
    this.isEditing = true;
    this.editingTaskGuid = task.UserGID,
      this.taskForm.patchValue({
        TaskTitle: task.taskTitle,
        TaskDescription: task.taskDescription,
        Priority: task.priority,
        TaskId:task.taskGID
      });
  }
  updateTask() {

    const task: Taskmodel = {
      UserGID: this.userGuid,
      TaskGID:this.taskForm.get('TaskId')?.value,
      TaskTitle: this.taskForm.get('TaskTitle')?.value,
      TaskDescription: this.taskForm.get('TaskDescription')?.value,
      Priority: this.taskForm.get('Priority')?.value
    }
    this.taskService.updateTask(task).subscribe({
      next:(res:any)=>{
         if (res.statuscode == 102) {
          alert(res.message);
          this.loadTasks();
          this.cancelEdit()
        }
      }
    })

  }
  deleteTask(task:any) {
    // const response = alert("Are You Sure You Wanted To Delete This Task"+task.taskTitle);
    const taskOBJ: Taskmodel = {
      UserGID: task.userGID,
      TaskGID:task.taskGID,
      TaskTitle: task.taskTitle,
      TaskDescription: task.taskDescription,
      Priority: task.priority,
      IsDeleted:true
    }
    const res = confirm('Are You Sure You Wanted To Delete This Task');
    if(!res){
      return;
    }
    this.taskService.deleteTask(taskOBJ).subscribe({
      next:(res:any)=>{
        if(res.statuscode == 101){
          
          if(res.valueOf)
          this.loadTasks();
        }
      }
    })

  }
  cancelEdit():void{
    this.isEditing=false;
    this.editingTaskGuid ='';
    this.taskForm.reset({Priority:1})
  }

  getPriority(num:number){
    if(num==1){
      return 'Low'
    }
    if(num==2){
      return 'Medium'
    }
    if(num==3){
      return 'High'
    }
    return 'unknown'
  }

}
