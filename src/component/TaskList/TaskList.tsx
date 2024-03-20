import PropTypes from "prop-types";
import styles from "./taskList.module.scss";
import { useState } from "react";
import { Todo } from "../../@types/todo.type";
interface TaskListProps {
  doneTaskList: boolean;
  todos: Todo[];
  handleDoneTodo: (id: string, done: boolean) => void;
}

export default function TaskList(props: TaskListProps) {
  const { doneTaskList, todos, handleDoneTodo } = props;
  console.log("todo", todos);
  return (
    <div className="mb-2">
      <h2 className={styles.title}>{doneTaskList ? "Hoàn thành" : "Chưa hoàn thành"}</h2>
      <div className={styles.tasks}>
        {todos.map((todo) => (
          <div className={styles.task} key={todo.id}>
            <input type="checkbox" className={styles.taskCheckbox} checked={todo.done} onChange={(event) => handleDoneTodo(todo.id, event.target.checked)} />
            <span className={` ${styles.taskName} ${todo.done ? styles.taskNameDone : ""}`}>{todo.name}</span>
            <div className={styles.taskActions}>
              <button className={styles.taskBtn}>🖊️</button>
              <button className={styles.taskBtn}>🗑️</button>
            </div>
          </div>
        ))}
      </div>
    </div>
  );
}
