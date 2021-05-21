import { onBeforeUnmount, Ref } from 'vue';

export function useDraggable(
  element: HTMLElement,
  draggable: Ref<boolean>,
  margin = 0
) {
  let x = 0;
  let y = 0;

  const onDrag = (e: MouseEvent) => {
    e.preventDefault();
    element.style.top = `${element.offsetTop + e.clientY - y - margin}px`;
    element.style.left = `${element.offsetLeft + e.clientX - x - margin}px`;
    x = e.clientX;
    y = e.clientY;
  };

  const removeListeners = () => {
    document.removeEventListener('mousemove', onDrag);
    document.removeEventListener('mouseup', removeListeners);
  };

  const onMouseDown = (e: MouseEvent) => {
    e.preventDefault();
    x = e.clientX;
    y = e.clientY;

    document.addEventListener('mousemove', onDrag);
    document.addEventListener('mouseup', removeListeners);
  };

  element.onmousedown = e => {
    if (e.altKey || e.shiftKey || e.metaKey || e.ctrlKey) {
      onMouseDown(e);
    }
  };

  element.onmouseenter = () => {
    element.focus();
  };

  element.onmouseleave = () => {
    element.blur();
    draggable.value = false;
  };

  element.onkeydown = e => {
    if (e.altKey || e.shiftKey || e.metaKey || e.ctrlKey) {
      draggable.value = true;
    }
  };

  element.onkeyup = () => {
    draggable.value = false;
  };

  onBeforeUnmount(() => {
    removeListeners();
  });
}
