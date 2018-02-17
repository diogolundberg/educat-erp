<template>
  <div class="relative mb2">
    <input type="text" required
      class="py2 border-none input-line transition h5 col-12"
      @input="$emit('input', $event.target.value)">
    <label class="py2 absolute top-0 transition h5 nudge">
      {{ label }}
    </label>
  </div>
</template>

<script>
  export default {
    name: "InputBox",
    props: {
      value: {
        type: String,
        required: true,
      },
      label: {
        type: String,
        required: true,
      },
    },
  };
</script>
