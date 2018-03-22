<template>
  <span>
    <template v-if="name == 'success'">
      <div class="f-modal-icon f-modal-success animate">
        <span class="f-modal-line f-modal-tip" />
        <span class="f-modal-line f-modal-long" />
        <div class="f-modal-placeholder" />
        <div class="f-modal-fix" />
      </div>
    </template>
    <template v-if="name == 'error'">
      <div class="f-modal-icon f-modal-error animate">
        <span class="f-modal-x-mark">
          <span class="f-modal-line f-modal-left animateXLeft" />
          <span class="f-modal-line f-modal-right animateXRight" />
        </span>
        <div class="f-modal-placeholder" />
        <div class="f-modal-fix" />
      </div>
    </template>
    <template v-if="name == 'warning'">
      <div class="f-modal-icon f-modal-warning">
        <span class="f-modal-body pulseWarningIns" />
        <span class="f-modal-dot pulseWarningIns" />
      </div>
    </template>
  </span>
</template>

<script>
  export default {
    name: "Animation",
    props: {
      name: {
        type: String,
        default: "success",
      },
    },
  };
</script>
