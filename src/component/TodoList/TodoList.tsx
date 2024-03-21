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

  //========================================================
  //EDIT
  const [currentTodo, setCurrentTodo] = useState<Todo | null>(null);
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
  const startEditTodo = (id: string) => {
    const findedTodo = todos.find((todo) => todo.id === id);
    if (findedTodo) {
      setCurrentTodo(findedTodo);
    }
  };
  const editTodo = (name: string) => {
    setCurrentTodo((prev) => {
      if (prev) {
        return { ...prev, name };
      }
      return null;
    });
  };
  const finishEditTodo = () => {
    setTodo((prev) => {
      return prev.map((todo) => {
        if (todo.id === currentTodo?.id) {
          return currentTodo;
        }
        return todo;
      });
    });
    setCurrentTodo(null);
  };
  return (
    <div className={styles.todoList}>
      <div className={styles.todoListContainer}>
        <TaskInput addTodo={addTodo} currentTodo={currentTodo} editTodo={editTodo} finishEditTodo={finishEditTodo} />
        <TaskList doneTaskList={false} todos={notdoneTodos} handleDoneTodo={handleDoneTodo} startEditTodo={startEditTodo} />
        <TaskList doneTaskList todos={doneTodos} handleDoneTodo={handleDoneTodo} startEditTodo={startEditTodo} />
      </div>
    </div>
  );
}

export default TodoList;
