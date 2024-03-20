import { Todo } from "../../@types/todo.type";
import TaskInput from "../TaskInput/TaskInput";
import TaskList from "../TaskList/TaskList";
import styles from "./todoList.module.scss";
import React, { useState } from "react";

function TodoList() {
  const [todos, setTodo] = useState<Todo[]>([]);
  const doneTodos = todos.filter((todo) => todo.done);
  console.log("11111111", doneTodos);

  const notdoneTodos = todos.filter((todo) => !todo.done);
  console.log("2222222222222", notdoneTodos);
  const addTodo = (name: string) => {
    const todo: Todo = {
      name,
      done: false,
      id: new Date().toISOString(),
    };
    setTodo((prev) => [...prev, todo]);
  };

  const handleDoneTodo = (id: string, done: boolean) => {
    console.log(done);
    setTodo((prev) => {
      return prev.map((todo) => {
        console.log("idtodo", todo.id);
        console.log("id", id);
        if (todo.id === id) {
          return { ...todo, done };
        }
        return todo;
      });
    });
  };
  //==============================================================
  //EDIT
  return (
    <div className={styles.todoList}>
      <div className={styles.todoListContainer}>
        <TaskInput addTodo={addTodo} />
        <TaskList doneTaskList={false} todos={notdoneTodos} handleDoneTodo={handleDoneTodo} />
        <TaskList doneTaskList todos={doneTodos} handleDoneTodo={handleDoneTodo} />
      </div>
    </div>
  );
}

export default TodoList;
